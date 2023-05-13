using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace CodeGenerationSample;

[Generator]
public class HelloWorldGenerator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        StringBuilder sourceBuilder = new(@"
                using system;
                namespace CodeGenerationSample;
                {
                    public static class HelloWorld
                    {
                        public static void Hello()
                        {
                            Console.WriteLine(""Hello from generated code!"");
                            Console.WriteLine(""The following source files existed in the compilation:"");
                    """);

        IEnumerable<SyntaxTree> syntaxTrees = context.Compilation.SyntaxTrees;
        foreach (SyntaxTree syntaxTree in syntaxTrees)
        {
            _ = sourceBuilder.AppendLine($@"Console.WriteLine(@""source file: {syntaxTree.FilePath}"");");
        }
        _ = sourceBuilder.Append(@"
                    }
                }
            }");

        context.AddSource("helloWorld", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        // This space intentionally left blank
    }
}
