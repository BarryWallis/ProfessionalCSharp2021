using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace CodeGenerationSample;

[Generator]
public class EquatableGenerator : ISourceGenerator
{
    private const string AttributeText =
    @"
        using System;
        namespace CodeGenerationSample
        [AttributeUsage(AttributeTargets.Class, Inherited =false, AllowMultiple =false)]
            sealed class ImplementEquatableAttribute : Attribute
            {
                public ImplementEquatableAttribute() { }
            }
";
    public void Execute(GeneratorExecutionContext context)
    {
        context.AddSource("ImplementEquatableAttribute", SourceText.From(AttributeText, Encoding.UTF8));
        if (context.SyntaxReceiver is not SyntaxReceiver syntaxReceiver)
        {
            return;
        }
        CSharpParseOptions? options = (context.Compilation as CSharpCompilation)?.SyntaxTrees[0].Options
                                      as CSharpParseOptions;
        Compilation compilation = context.Compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText(SourceText.From(
            AttributeText,
            Encoding.UTF8), options));
        INamedTypeSymbol? attributeSymbol 
            = compilation.GetTypeByMetadataName("CodeGenerationSample.ImplementEquatableAttribute");

        List<ITypeSymbol> typeSymbols = new();
        foreach (ClassDeclarationSyntax @class in syntaxReceiver.CandidateClasses)
        {
            SemanticModel semanticModel = compilation.GetSemanticModel(@class.SyntaxTree);
            INamedTypeSymbol? typeSymbol = semanticModel.GetDeclaredSymbol(@class);
            Debug.Assert(typeSymbol != null);
            if (typeSymbol!.GetAttributes().Any(attr
                => attr.AttributeClass!.Equals(attributeSymbol, SymbolEqualityComparer.Default)))
            {
                typeSymbols.Add(typeSymbol);
            }
        }

        foreach (INamedTypeSymbol typeSymbol in typeSymbols.Cast<INamedTypeSymbol>())
        {
            string classSource = GetClassSource(typeSymbol);
            context.AddSource(typeSymbol.Name, SourceText.From( classSource, Encoding.UTF8));
        }
    }

    private string GetClassSource(INamedTypeSymbol typeSymbol)
    {
        string namespaceName = typeSymbol.ContainingNamespace.ToDisplayString();
        StringBuilder source = new(
        $@"
            using System;
            namespace {namespaceName}
            public partial class {typeSymbol.Name} : IEquatable<{typeSymbol.Name}>
            {{
                private static partial bool IsTheSame({typeSymbol.Name}? left, {typeSymbol.Name}? right);

                public override bool Equals(object? obj) => this == obj as {typeSymbol.Name};

                public bool Equals({typeSymbol.Name}? other) => this == other;

                public static bool operator==({typeSymbol.Name}? left, {typeSymbol.Name}? right) 
                    => IsTheSame(left, right);

                public static bool operator!=({typeSymbol.Name}? left, {typeSymbol.Name}? right)
                    => !(left == right);
            }}
        ");

        return source.ToString();
    }

    public void Initialize(GeneratorInitializationContext context)
        => context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
}
