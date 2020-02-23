using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using TestHelper;
using Xunit;

namespace EFCore.Internal.Analyzers.Test
{
    public class ExceptionsUseResourceStringsAnalyzerTests : DiagnosticVerifier
    {
        [ConditionalFact]
        public void ThrowExceptionUsingResourceString_NoResults()
        {
            var test = @"
    using System;

    namespace ConsoleApplication1
    {
        class TypeName
        {   
            void Method()
            {
                throw new Exception(CoreStrings.MyMessage);
            }
        }

        class CoreStrings
        {
            string MyMessage { get; set; } = "";
        }
    }";

            VerifyCSharpDiagnostic(test);
        }

        [ConditionalFact]
        public void ThrowExceptionUsingStringLiteral_Warning()
        {
            var test = @"
    using System;

    namespace ConsoleApplication1
    {
        class TypeName
        {   
            void Method()
            {
                throw new Exception(""Oh no!"");
            }
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "EFI1001",
                Message = "Exception message parameter should use resource strings",
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 10, 23)
                        }
            };

            VerifyCSharpDiagnostic(test, expected);

    //        var fixtest = @"
    //using System;
    //using System.Collections.Generic;
    //using System.Linq;
    //using System.Text;
    //using System.Threading.Tasks;
    //using System.Diagnostics;

    //namespace ConsoleApplication1
    //{
    //    class TypeName
    //    {   
    //        void Method()
    //        {
    //            throw new Exception(""Oh no!"");
    //        }
    //    }
    //}";
    //        VerifyCSharpFix(test, fixtest);
        }
        
        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new ExceptionsShouldUseResourceStringsAnalyzer();
        }
    }
}
