using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathtastic_Voyage {
    class Queue <T> {
        private Node <T> _head;
        private Node <T> _tail;
        private int _size =  0;

        public Queue() {
            _head = null;
            _tail = null;
            _size = 0;
        }

        public int GetSize {
            get{return _size;}
        }

        public bool Empty {
            get { return _head == null; }
        }//end property

        public void Enqueue(T new_data) {
            if (_head == null) {//then 
                _head = new Node <T>(new_data);
                //since it is the only node so far the head will equal the tail
                _tail = _head;
                _size++;
            }else{
                //if a head is no longer null then any new data will be added onto the tail
                _tail.Next = new Node <T> (new_data);
                _tail = _tail.Next;
                _size++;
            }//end if
        }//end Add()

        public T Dequeue() {
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
                throw new Exception("Queue empty");
            }//end if
        }//end method

        public T Peek() {
            //if the queue is not empty then return whatever is at the head
            if (!Empty) {
                return _head.Data;
            }else{
                throw new Exception("Queue empty");
            }//end if
        }//end method


        public T Clear() {
            //set the node to the head of the queue
            Node <T> current_node = _head;
            //while stepping through the queue it will clear the data 
            while(current_node != null) {
                current_node.Data = default;
                current_node = current_node.Next;
            }
            //the head is set back to the current node and should return null
            current_node = _head;

            return current_node.Data;
        }

        private T Get(int target_index) {
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

        public override string ToString() {
           string updated_string = "";
           string word;

            for (int i = _size-1; i >= 0 ; i--) {
                word = Convert.ToString(Get(i));
                if (i >= 1) {
                    updated_string += $"{word} ";
                }
                else {
                    updated_string += $"{word}";
                }
            }
            return $"{updated_string}";
        }
        
    }
}
