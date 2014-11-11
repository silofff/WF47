using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WF47
{
    public partial class Form1 : Form
    {
        readonly char[] digits = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        private readonly char[] add = { '+', '-' };
        private readonly char[] mul = { '*', '/' };
        private char currentChar;
        private State currentStackState = State.State1;
        private int id = 0;
        private IDictionary<string, State> stack = new Dictionary<string, State>();
        public enum State
        {
            State1,
            State2,
            State3,
            State4,
            State5,
            State6,
            State7,
            State8,
            State9,
            State10,
            State11
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void StackShow()
        {
            foreach (var t in stack.Keys)
            {
                textBox2.Text += t + " ";
            }
            textBox2.Text += "\r\n";
        }

        private char GetNextChar()
        {
            if (id == textBox1.TextLength) return 'e';
            return textBox1.Text[id++];
        }

        private bool IsTrueStr()
        {
            var state = currentStackState;
            currentChar = GetNextChar();
            while (true)
            {
                switch (state)
                {
                    case State.State1:
                        {
                            if (digits.Contains(currentChar))
                            {
                                stack.Add(currentChar.ToString(), currentStackState);
                                currentStackState = State.State1;
                                currentChar = GetNextChar();
                                state = State.State2;
                                StackShow();
                                break;
                            }
                            return false;
                        }
                    case State.State2:
                        {
                            stack.Remove(stack.Last());
                            stack.Add("DIGIT", currentStackState);
                            StackShow();
                            if (new State[] { State.State1, State.State10, State.State7 }.Contains(currentStackState))
                            {
                                state = State.State3;
                                break;
                            }
                            if (new State[] { State.State4, State.State9 }.Contains(currentStackState))
                            {
                                state = State.State5;
                                break;
                            }
                            return false;
                        }
                    case State.State3:
                        {
                            stack.Remove(stack.Last());
                            stack.Add("NUM", currentStackState);
                            StackShow();
                            if (new State[] { State.State1, State.State10 }.Contains(currentStackState))
                            {
                                state = State.State4;
                                break;
                            }
                            if (State.State7 == currentStackState)
                            {
                                state = State.State9;
                                break;
                            }
                            return false;
                        }
                    case State.State4:
                        {
                            if (digits.Contains(currentChar))
                            {
                                stack.Add(currentChar.ToString(), currentStackState);
                                StackShow();
                                currentStackState = State.State4;
                                currentChar = GetNextChar();
                                state = State.State2;
                                break;
                            }
                            else
                            {
                                stack.Remove(stack.Last());
                                stack.Add("TERM", currentStackState);
                                StackShow();
                                if (State.State1 == currentStackState)
                                {
                                    state = State.State6;
                                    break;
                                }
                                if (State.State10 == currentStackState)
                                {
                                    state = State.State11;
                                    break;
                                }
                            }
                            return false;
                        }
                    case State.State5:
                        {
                            stack.Remove(stack.Last());
                            currentStackState = stack.Last().Value;
                            stack.Remove(stack.Last());
                            stack.Add("NUM", currentStackState);
                            StackShow();
                            if (State.State1 == currentStackState || State.State10 == currentStackState)//???
                            {
                                state = State.State4;
                                break;
                            }
                            if (State.State7 == currentStackState)
                            {
                                state = State.State9;
                                break;
                            }
                            return false;
                        }
                    case State.State6:
                        {
                            if (mul.Contains(currentChar))
                            {
                                stack.Add(currentChar.ToString(), currentStackState);
                                StackShow();
                                currentStackState = State.State6;
                                currentChar = GetNextChar();
                                state = State.State7;
                                break;
                            }
                            else
                            {
                                stack.Remove(stack.Last());
                                stack.Add("EXTR", currentStackState);
                                StackShow();
                                if (State.State1 == currentStackState)
                                {
                                    state = State.State8;
                                    break;
                                }
                            }
                            return false;
                        }
                    case State.State7:
                        {
                            if (digits.Contains(currentChar))
                            {
                                stack.Add(currentChar.ToString(), currentStackState);
                                StackShow();
                                currentStackState = State.State7;
                                currentChar = GetNextChar();
                                state = State.State2;
                                break;
                            }
                            return false;
                        }
                    case State.State8:
                        {
                            if (currentChar == 'e')
                            {
                                return true;
                            }
                            if (add.Contains(currentChar))
                            {
                                stack.Add(currentChar.ToString(), currentStackState);
                                StackShow();
                                currentStackState = State.State8;
                                currentChar = GetNextChar();
                                state = State.State10;
                                break;
                            }
                            return false;
                        }
                    case State.State9:
                        {
                            if (digits.Contains(currentChar))
                            {
                                stack.Add(currentChar.ToString(), currentStackState);
                                StackShow();
                                currentStackState = State.State9;
                                currentChar = GetNextChar();
                                state = State.State2;
                                break;
                            }
                            else
                            {
                                stack.Remove(stack.Last());
                                stack.Remove(stack.Last());
                                currentStackState = stack.Last().Value;
                                stack.Remove(stack.Last());
                                stack.Add("TERM", currentStackState);
                                StackShow();
                                if (State.State1 == currentStackState)
                                {
                                    state = State.State6;
                                    break;
                                }
                                if (State.State10 == currentStackState)
                                {
                                    state = State.State11;
                                    break;
                                }
                            }
                            return false;
                        }
                    case State.State10:
                        {
                            if (digits.Contains(currentChar))
                            {
                                stack.Add(currentChar.ToString(), currentStackState);
                                StackShow();
                                currentStackState = State.State10;
                                currentChar = GetNextChar();
                                state = State.State2;
                                break;
                            }
                            return false;
                        }
                    case State.State11:
                        {
                            if (mul.Contains(currentChar))
                            {
                                stack.Add(currentChar.ToString(), currentStackState);
                                StackShow();
                                currentStackState = State.State11;
                                currentChar = GetNextChar();
                                state = State.State7;
                                break;
                            }
                            else
                            {
                                stack.Remove(stack.Last());
                                stack.Remove(stack.Last());
                                currentStackState = stack.Last().Value;
                                stack.Remove(stack.Last());
                                stack.Add("EXTR", currentStackState);
                                StackShow();
                                if (State.State1 == currentStackState)
                                {
                                    state = State.State8;
                                    break;
                                }
                            }
                            return false;
                        }
                }
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox1.BackColor = IsTrueStr() ? Color.LightGreen : Color.Salmon;
            if (textBox1.TextLength == 0) textBox1.BackColor = Color.White;
            id = 0;
            currentStackState = State.State1;
            stack.Clear();
        }
    }
}
