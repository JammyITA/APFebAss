using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions; //Regex.Replace

namespace APFebAss
{
    class Compiler
    {

        Node root;
        string input;

        public Compiler(string input, Node root)
        {
            this.input = input;
            this.root = root;
        }


        public string compile()
        {
            Console.WriteLine("Compiling:\n\t" + input);
            try
            {
                root.eval();
            }
            catch (Exception e)
            {
                Console.WriteLine("Compilation failed.\n" + e.Message);
                Environment.Exit(1);
            }

            StringBuilder compiledCode = new StringBuilder();

            compiledCode.AppendLine("//Warning: this code is incopatible with ISO C90 standard");
            compiledCode.AppendLine("//tested with gcc 4.0.4");
            compiledCode.AppendLine("#include <stdio.h>");
            compiledCode.AppendLine("#include <stdlib.h>");
            compiledCode.AppendLine("typedef int bool;");
            compiledCode.AppendLine("#define True 1");
            compiledCode.AppendLine("#define False 0");
            compiledCode.AppendLine("int main(){");

            compiledCode.AppendLine("bool result = True;");
            compiledCode.AppendLine("char input[] = \"" + input + "\";");

            root.compile(compiledCode);

            compiledCode.AppendLine("printf(\"Evaluating:\\n\\t %s \\nResult: %s\\t\", input, result ? \"True\" : \"False\");");
            compiledCode.AppendLine("\treturn 0;");
            compiledCode.AppendLine("}");

            //Console.WriteLine("\n\n****************\n" + compiledCode + "\n****************\n\n");


            //return compiledCode.ToString();
            return deleteResultLines(compiledCode.ToString());

        }

        //With this function I want to delete the unnecessary lines with only  a "result" variable, generated
        //by a nested "and" with some "let" inside. Considered his cost, I decide to rely on the optimization
        //performed by c's compiler and not use this function.
        string deleteResultLines(String s)
        {
            return Regex.Replace(s, "result = result[ && result]*[ ]*;", "");
        }

    }

}