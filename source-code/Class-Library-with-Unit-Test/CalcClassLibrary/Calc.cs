using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalcClassLibrary
{
    public class Calc
    {
        public int Average(int[] nums)
        {
            int total = 0;
            foreach (var num in nums)
            {
               total += num;
            }
             return total / nums.Count();
            
        }

        public int Largest(int[] nums)
        {
            int largest = nums[0];
            foreach (var num in nums)
            {
                if (num > largest)
                {
                    largest = num;
                }
            }
            return largest;
        }

        public int Smallest(int[] nums)
        {
            int smallest = nums[0];
            foreach (var num in nums)
            {
                if (num < smallest)
                {
                    smallest = num;
                }
            }
            return smallest;
        }
        
    }
}
