using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathtastic_Voyage {
    class MathProblem <T> {
        private string _correct;
        private string _user;
        private string _infix;
        private string _postfix;

        public MathProblem(string data) {
            _infix= data;
            _postfix = "";
        }

        public MathProblem() {
            _infix = "";
            _postfix = "";
        }

        public string Correct {
            get {return _correct;}
            set {_correct = value;}
        }

        public string User {
            get {return _user;}
            set {_user = value;}
        }

        public string Infix {
            get{return _infix;}
            set{_infix = value;}
        }

        public string Postfix {
            get{return _postfix;}
            set{_postfix = value;}
        }
  
    // The main method that converts given infix expression  
    // to postfix expression.   
        public string[] FixInfixLayout(string infix) {
            char[] infix_array = new char[infix.Length];
            infix_array = infix.ToCharArray();
            string final_infix = "";
            int count = 0;

            foreach(char item in infix_array) {
                if(item == '-' && infix_array[count + 1] >= 0 || item == '-' && infix_array[count + 1] <= 9) {
                    final_infix += $" {item} ";
                }
                else if (item == '+' || item == '-' || item == '*' || item == '/' || item == '^' || item == '(' || item == ')') {
                    final_infix += $" {item}";
                }
                else {
                    final_infix += $"{item}";
                }//end if
                count++;
            }

            _infix = final_infix;
            char[] separators = { ' ', '\t', '\n', };
            string[] infix_string = _infix.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            
            return infix_string;
        }

        public void ConverttoPostFix(string[] infix) {
            Stack<string> Stack = new Stack<string>();
            _postfix = "";

            for (int index = 0; index < infix.Length; index++) {
                if (IsOperator(infix[index])) {
                    while(!Stack.Empty && Stack.Peek != "(" && HasHigherPrecedence(Stack.Peek, infix[index])){
                        _postfix += Stack.Pop();
                    }//end while
                    Stack.Push(infix[index]);
                }
                else if (IsNumeric(infix[index])) {
                    _postfix += $" {infix[index]} ";
                }
                else if (IsLeftParenthesis(infix[index])) {
                    Stack.Push(infix[index]);
                }
                else if (IsRightParenthesis(infix[index])) {
                    while(Stack.Peek != null && Stack.Peek != "(") {
                        _postfix += $" {Stack.Pop()} ";
                    }
                    Stack.Pop();
                }//end if
            }//end for

            while (!Stack.Empty) {
                _postfix += $" {Stack.Pop()} ";
            }//end while
        }//end method

        private bool IsNumeric(string c) {
            double temp;
            return double.TryParse(c, out temp);
        }

        private bool IsOperator(string c) {
            bool confirm = false;
            if (c == "+" || c == "-" || c == "*" || c == "/" || c == "^") {
                confirm = true;
            }
           return confirm;          
        }

        private bool HasHigherPrecedence(string op1, string op2) {
            int op1Weight = GetOperatorWeight(op1);
            int op2Weight = GetOperatorWeight(op2);

            if(op1Weight == op2Weight) {
                if(IsRightAssociative(op1)) return false;
                else return true;
            }
            return op1Weight > op2Weight ? true : false;

        }//end method

        private bool IsRightAssociative(string op) {
            if(op == "^") {
                return true;
            }//end if
            return false;
        }//end method

        private int GetOperatorWeight(string op) {
            int weight = -1;

            if(op == "+" || op == "-") {
                weight = 1;
            }
            else if (op == "*" || op == "/") {
                weight = 2;
            }
            else if (op == "^") {
                weight = 3;
            }//end if

            return weight;
        }

        private bool IsLeftParenthesis(string data) {
            if(data == "(") {
                return true;
            }//end if
            return false;
        }//end method

        private bool IsRightParenthesis(string data) {
            if(data == ")") {
                return true;
            }//end if
            return false;
        }

        public bool CheckUserCorrect() {
            if(_user == _correct) {
                return true;
            } else {
                return false;
            }
        }


        public bool StringCheck(string[] data) {
            bool valid = false;
             if(data.Length < 3) {
                valid = false;
            } else {
                valid = CheckIfValid(data);
            }//end if
             return valid;
        }

        private bool CheckIfValid(string[] data) {
            int num_count = 0;
            int op_count  = 0;
            int count = 1;

            foreach (string  oper in data) {
                if(count == 1 && !IsNumeric(oper) || count == 2 && !IsNumeric(oper)) {
                    return false;
                }
                count++;
            }//end foreach

            foreach(string oper in data) {
                if (IsNumeric(oper)) {
                    num_count++;
                }
                else if(oper == "+" || oper == "-" || oper == "*" || oper == "/") {
                    op_count++;
                }
                else {
                    return false;
                }

            }//end foreach

            return (num_count - op_count == 1);
        }
    }
}
