using System;
using System.Collections.Generic;
using System.Text;

namespace Mathtastic_Voyage{
    class Stack <T> {
        private Node<T> _head;
        private int _size = 0;

        public int Size {
            get {return _size; }
        }//end property

        public bool Empty {
            get { return _head == null; }
        }//end property

        public T Peek {
            get {return _head.Data;}
        }//end property

        public Stack() {
            _head = null;
        }//end constructor

        public void Push(T new_data) {
            Node<T> new_node = new Node<T>(new_data);
            new_node.Next = _head;
            _head = new_node;
            _size += 1;
        }//end method

        public T Pop() {
            if (!Empty) {
                //STORE HEAD'S DATA
                T return_data = _head.Data;
                
                //REMOVE NODE
                _head = _head.Next;

                //UPDATE SIZE
                _size -= 1;

                //RETURN DATA
                return return_data;
            } else {
                throw new Exception("Stack empty");
            }//end if
        }//end method

        

        public T Get(int target_index) {
                    int current_index = 0;
        
                    // START @ HEAD
                    Node <T> current_node = _head;
        
                    // TRAVERSE THE LIST UNTIL THE END
                    while (current_node != null) {
                        //IF FOUND RETURN
                        if (current_index == target_index) {
                            return current_node.Data;
                        }//end if
        
                        //GOTO TO NEXT NODE AND INCREMENT INDEX
                        current_node = current_node.Next;
                        current_index += 1;
                    }//end while
        
                    //IF NOT IN LIST THROW EXCEPTION
                    throw new IndexOutOfRangeException("index[" + target_index + "] is not present in this Linked List");
                }//end Get()

        
    }//end class
}
