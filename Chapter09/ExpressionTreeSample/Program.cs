// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Linq.Expressions;

using DataLib;

Expression<Func<Racer, bool>> expression = r => r.Country == "Brazil" && r.Wins > 6;
DisplayTree(0, "Lambda", expression);

static void DisplayTree(int indent, string message, Expression expression)
{
    string output = $"{string.Empty.PadLeft(indent, '>')} {message}! NodeType: {expression.NodeType}; " +
                    $"Expr: {expression}";
    indent += 1;

    switch (expression.NodeType)
    {
        case ExpressionType.Constant:
            ConstantExpression constantExpression
                = expression as ConstantExpression
                  ?? throw new InvalidCastException("Expected ConstantExpression");
            Console.WriteLine($"{output} Const Value: {constantExpression.Value}");
            break;
        case ExpressionType.Equal:
        case ExpressionType.AndAlso:
        case ExpressionType.GreaterThan:
            BinaryExpression binaryExpression = expression as BinaryExpression
                                                ?? throw new InvalidCastException("Expected BinaryExpression");
            if (binaryExpression.Method is not null)
            {
                Debug.Assert(binaryExpression.Method is not null);
                Console.WriteLine($"{output} Method: {binaryExpression.Method.Name}");
            }
            else
            {
                Console.WriteLine(output);
            }
            DisplayTree(indent, "Left", binaryExpression.Left);
            DisplayTree(indent, "Right", binaryExpression.Right);
            break;
        case ExpressionType.Lambda:
            Console.WriteLine(output);
            LambdaExpression lambda = expression as LambdaExpression
                                      ?? throw new InvalidCastException("Expected LambdaExpression");
            foreach (ParameterExpression parameter in lambda.Parameters)
            {
                DisplayTree(indent, "Parameter", parameter);
            }
            DisplayTree(indent, "Body", lambda.Body);
            break;
        case ExpressionType.MemberAccess:
            MemberExpression memberExpression = expression as MemberExpression
                                                ?? throw new InvalidCastException("Expected MemberExpression");
            Console.WriteLine($"{output} Member Name: {memberExpression.Member.Name}, " +
                $"Type: {memberExpression.Type}");
            Debug.Assert(memberExpression.Expression is not null);
            DisplayTree(indent, "Member Expr", memberExpression.Expression);
            break;
        case ExpressionType.Parameter:
            ParameterExpression parameterExpression
                = expression as ParameterExpression
                  ?? throw new InvalidCastException("Expected ParameterExpression");
            Console.WriteLine($"{output} Param Type: {parameterExpression.Type.Name}");
            break;
        default:
            Console.WriteLine();
            Console.WriteLine($"{expression.NodeType} {expression.Type.Name}");
            break;
    }
}
