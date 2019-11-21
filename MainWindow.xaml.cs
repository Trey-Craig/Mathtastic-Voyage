using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Mathtastic_Voyage {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        Queue<string> queue = new Queue<string>();
        MathProblem<string> maths = new MathProblem<string>();
        static int correct = 0;
        static int incorrect = 0;

        public MainWindow() {
            InitializeComponent();
            UpdateQuestions();
            blkdisplay.Text = $"{queue.Peek()} =";
        }//end constructor

        private void btn_submit_Click(object sender, RoutedEventArgs e) {  
            

            maths.ConverttoPostFix(maths.FixInfixLayout(queue.Peek()));

            char[] split_args = { ' ', '\t', '\n'};

            maths.Correct = PostFix(maths.Postfix.Split(split_args, StringSplitOptions.RemoveEmptyEntries));
            maths.User = txtans.Text;

            if(maths.Correct == maths.User) {
                correct++;
                lst_correct.Items.Add(queue.Peek() + $"= {maths.User}");
                lst_problems.Items.Remove(queue.Peek());
                queue.Dequeue();

            }
            else {
                incorrect++;
                lst_incorrect.Items.Add(queue.Peek() + $"= {maths.User}");
                lst_problems.Items.Remove(queue.Peek());
                queue.Dequeue();
            }


            if(queue.Empty) {
                int score = 0;
                int total = correct + incorrect;
                score = correct / total;
                blkscore.Text = Convert.ToString(score *100)+ "%";
                btn_submit.IsEnabled = false;
            }
            else {
                blkdisplay.Text = $"{queue.Peek()} =";
                txtans.Clear();
            }
            


        }

        private void UpdateQuestions() {
            string[] problems = ReadFile();
            foreach (string item in problems) {
                queue.Enqueue(item);
                lst_problems.Items.Add(item);
            }
        }

        private string[] ReadFile() {
            FileStream input_file;
            string file_name = "C:\\Users\\MCA\\Downloads\\Mathtastic Voyage";
            
            string[] file_text = File.ReadAllLines(file_name);

            return file_text;
        }

        private string PostFix(string[] text) {
            //split input into a string array
            

            //create new Stack variable
            Stack <string> new_stack = new Stack<string>();

            //Variables
            string operand1 = "";
            string operand2 = "";
            string result = "";
            
            for (int i = 0; i < text.Length; i++) {
                //if split_text is equal to an operator....
                if (ContainsOperator(text[i])) {
                    //....Pop and Store values
                    operand2 = new_stack.Pop();
                    operand1 = new_stack.Pop();

                    //Evaluate to get new value then Push it onto the stack 
                    result = Evaluate(operand1,operand2,text[i]);
                    new_stack.Push(result);                    
                }
                //if split_text is not an operator...
                else {
                    //....split_text is pushed onto stack
                    new_stack.Push(text[i]);
                }
            } 
            //return top of stack to be posted in the results box
            return new_stack.Peek;
        }

        private bool ContainsOperator(string text) {
            bool confirm = false;

            if (text == "+" || text == "-" || text == "*" || text == "/" || text == "x") {
                confirm = true;
            }
           return confirm;         
        }

        private string Evaluate(string operand1, string operand2, string symbol) {
            decimal new_answer = 0;
            decimal operand_1 = 0;
            decimal operand_2 = 0;
            //Convert operands into decimal
            operand_2 = Convert.ToDecimal(operand2);
            operand_1 = Convert.ToDecimal(operand1);
            if (symbol == "+") {
                new_answer = operand_1 + operand_2;
            }
            else if (symbol == "-") {
                new_answer = operand_1 - operand_2;
            }
            else if (symbol == "*" || symbol == "x") {
                new_answer = operand_1 * operand_2;
            }
            else if (symbol == "/") {
                new_answer = operand_1 / operand_2;
            }
            return Convert.ToString(new_answer);
        }
    }
}
