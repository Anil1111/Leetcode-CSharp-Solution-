﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math_Question
{
    class MathQuestions
    {
        static void Main(string[] args)
        {
            Console.WriteLine(2 / 3);
        }
        #region Extension: Sum all the digits
        public int SumDigits(int n)
        {
            int sum = 0;
            while (n != 0)
            {
                sum += n % 10;
                n /= 10;
            }
            return sum;
        }
        #endregion
        #region Leetcode 7  Reverse Integers
        public int Reverse(int x)
        {
            int ans = 0;
            while (x != 0)
            {
                int temp = ans * 10 + x % 10;
                if (temp / 10 != ans) // If temp overflows MaxValue, it will become MinValue
                {
                    return 0;
                }
                ans = temp;
                x /= 10;
            }
            return ans;
        }
        #endregion
        #region Leetcode 12  Integer to Roman
        public string IntToRoman(int num)
        {
            StringBuilder res = new StringBuilder("");
            string[] roman = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            int[] value = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            int index = 0;
            while (num > 0)
            {
                int count = num / value[index];
                for (int i = 0; i < count; i++)
                {
                    res.Append(roman[index]);
                }
                num %= value[index];
                index++;
            }
            return res.ToString();
        }
        #endregion
        #region Leetcode 13  Roman to Integer
        public int RomanToInt(string s)
        {
            Dictionary<char, int> roman = new Dictionary<char, int>();
            roman.Add('M', 1000);
            roman.Add('D', 500);
            roman.Add('C', 100);
            roman.Add('L', 50);
            roman.Add('X', 10);
            roman.Add('V', 5);
            roman.Add('I', 1);
            int sum = 0;
            for (int i = 0; i < s.Length - 1; i++)
            {
                // Normally, the current character should be bigger than the right character
                if (roman[s[i]] < roman[s[i + 1]])
                {
                    sum -= roman[s[i]];
                }
                else
                {
                    sum += roman[s[i]];
                }
            }
            // We are not able to calculate the value of the last character in the loop because s[i+1] would be out of bound
            // However, it is guaranteed to be positive
            return sum + roman[s[s.Length - 1]];

        }
        #endregion
        #region Leetcode 67  Add Binary
        public string AddBinary(string a, string b)
        {
            StringBuilder res = new StringBuilder("");
            int i = a.Length - 1;
            int j = b.Length - 1;
            int carry = 0;
            while (i >= 0 || j >= 0)
            {
                int sum = carry;
                if (i >= 0)
                {
                    sum += a[i--] - '0';
                }
                if (j >= 0)
                {
                    sum += b[j--] - '0';
                }
                // In base 10, if we have 14, our current sum is 14 % 10 = 4
                // Our carry is 14 / 10
                res.Append(sum % 2);
                carry = sum / 2;

            }
            if (carry != 0)
            {
                res.Append(1);
            }
            // In our program, we do the addition from the largest digit while in real life we do it from the smallest one
            // Therefore, we have to reverse our result
            return new string(res.ToString().Reverse().ToArray());
        }
        #endregion
        #region Leetcode 168  Excel Sheet Column Title
        public string ConvertToTitle(int n)
        {
            StringBuilder res = new StringBuilder();
            while (n > 0)
            {
                n--;
                // Note that we are using 'A' + n % 26 but 'A' itself stands for one so we have to n--
                res.Append((char)('A' + n % 26));
                n /= 26;
            }

            return new String(res.ToString().Reverse().ToArray());
        }
        #endregion
        #region Leetcode 171  Excel Sheet Column Number
        public int TitleToNumber(string s)
        {
            int res = 0;
            for (int i = 0; i < s.Length; ++i)
            {
                res *= 26;
                res += (s[i] - 'A' + 1);
            }
            return res;
        }
        #endregion
        #region Leetcode 172  Factorial Trailing Zero
        public int TrailingZeroes(int n)
        {
            // A trailing zero can only be made 
            // In a n! operation, there is always more 2 than 5
            // Therefore, we just have to count the amount of 5
            return n == 0 ? 0 : n / 5 + TrailingZeroes(n / 5);
        }
        #endregion
        #region Leetcode 258  Add Digits
        public int AddDigits(int num)
        {
            while (num % 10 != 0)
            {
                num = SumDigits(num);
            }
            return num;
        }
        #endregion
        #region Leetcode 313  Super Ugly Digits
        public int NthSuperUglyNumber(int n, int[] primes)
        {
            int[] ugly = new int[n];
            int[] index = new int[primes.Length];
            ugly[0] = 1;
            for (int i = 1; i < n; ++i)
            {
                ugly[i] = int.MaxValue;
                for (int j = 0; j < primes.Length; ++j)
                {
                    ugly[i] = System.Math.Min(ugly[i], primes[j] * ugly[index[j]]);
                }
                for (int k = 0; k < primes.Length; ++k)
                {
                    if (ugly[i] >= primes[k] * ugly[index[k]])
                    {
                        ++index[k];
                    }
                }
            }
            return ugly[n - 1];
        }
        #endregion
        #region Leetcode 326  Power of Three
        public bool IsPowerOfThree(int n)
        {
            if (n < 1)
            {
                return false;
            }
            while (n % 3 == 0)
            {
                n /= 3;
            }
            return n == 1;
        }
        #endregion
        #region Leetcode 202  Happy Number
        public bool IsHappy(int n)
        {
            // Using Floyd's Cycle Detection(Two Pointers)
            if (n == 1) { return true; }
            int fast = SumDigitSquare(n);
            int slow = n;
            while (slow != fast)
            {
                if (fast == 1 || slow == 1) { return true; }
                slow = SumDigitSquare(slow);
                fast = SumDigitSquare(SumDigitSquare(fast));

            }
            return false;
        }
        public int SumDigitSquare(int n)
        {
            int sum = 0;
            while (n != 0)
            {
                int cur = n % 10;
                sum += cur * cur;
                n /= 10;
            }
            return sum;
        }
        #endregion
        #region Leetcode 367  Valid Perfect Square
        // 1 + 3 + 5 + ... + (2i - 1) = (2i - 1 + 1) * (i/2) = i * i
        public bool IsPerfectSquare(int n)
        {
            int i = 1;
            while (n > 0)
            {
                n -= i;
                i += 2;
            }
            return n == 0;
        }
        #endregion
        #region Leetcode 517  Super Washing Machine
        public int FindMinMoves(int[] machines)
        {
            int sum = machines.Sum();
            int n = machines.Length;
            if(sum % n != 0) { return -1; }
            int target = sum / n;
            int over = 0;
            int ans = 0;
            foreach(int m in machines)
            {
                over += m - target;
                ans = Math.Max(ans, Math.Max(m - target, Math.Abs(over)));
                // If over is positive, than we definitely need to do more than over operations
                // If over is negative, then we do not really need over operations 
                // since clothes can be transfered from two of its adjacent machines

                // If the current machine has more clothes than its target, then it also needs more than m - target operations

            }
            return ans;
        }
        #endregion
        #region Leetcode 400  Nth Digit
        // From 1 ~ 9, there are 9 one digit numbers = 1 * 9 digits
        // From 10 ~ 99, there are 90 two digits numbers = 2 * 90 digit

        // After the while loop, we know that our target digit is the nth(after loop) digit in all (len) digit number

        // Then, we do start += (n-1) / len to specify which number is it

        // Finally, we mod(n-1) with len to find the actual index
        public int FindNthDigit(int n)
        {
            long range = 9;
            int len = 1;
            int start = 1;
            // The start of the range
            while (n > len * range)
            {
                n -= (int)(len * range);
                ++len;
                range *= 10;
                start *= 10;
            }
            // Since we are referring to the index, we should use n-1

            // For example, we are trying to the third digit in the 3-digits numbers
            // The start should be 100 instead of 101
            // And the index of the third digit should be 2, which is obtained by (3-1) % 3

            start += (n - 1) / len;
            return (int)Char.GetNumericValue(Convert.ToString(start)[(n - 1) % len]);
        }
        #endregion
        #region Leetcode 413  Arithmetic Slices
        public int NumberOfArithmeticSlices(int[] A)
        {
            // dp[i] the amount of slices ending with A[i]
            int ans = 0;
            int count = 0;
            // count represents dp[i-1]
            for (int i = 2; i < A.Length; ++i)
            { // We at least need three numbers to form a slice
                if (A[i] - A[i - 1] == A[i - 1] - A[i - 2])
                {
                    ++count;
                    ans += count;
                }
                else
                {
                    count = 0;
                }
            }
            return ans;
        }
        #endregion
        #region Leetcode 423  Reconstruct Original Digits from English
        public string OriginalDigits(string s)
        {
            int[] count = new int[10];
            foreach(char c in s)
            {
                if(c == 'z')
                {
                    count[0]++;
                }
                if(c == 'w')
                {
                    count[2]++;
                }
                if(c == 'x')
                {
                    count[6]++;
                }
                if(c == 's')
                {
                    count[7]++;
                    // six & seven
                    // Therefore, actual seven count + count[6] = count[7]
                }
                if(c == 'g')
                {
                    count[8]++;
                }
                if(c == 'u')
                {
                    count[4] ++;
                }
                if(c == 'f')
                {
                    count[5]++;
                    // five & four
                }
                if(c == 'h')
                {
                    count[3]++;
                    // three & eight
                }
                if(c == 'o')
                {
                    count[1]++;
                    // one & four & two & zero
                }
                if(c == 'i')
                {
                    count[9]++;
                    // nine & eight & six & five
                }
            }
            // Offset
            count[7] -= count[6];
            count[5] -= count[4];
            count[3] -= count[8];
            count[1] -= (count[4] + count[2] + count[0]);
            count[9] -= (count[8] + count[6] + count[5]);

            StringBuilder res = new StringBuilder();

            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j < count[i]; j++)
                {
                    res.Append(i);
                }
            }
            return res.ToString();
        }
        #endregion
        #region Leetcode 453  Minimum Moves to Equal Array Elements
        public int MinMoves(int[] nums)
        {
            // Adding 1 to n-1 elements is the same as reducing 1 from 1 element
            // Therefore, the best way is to make all other element the same as the min elment
            if (nums.Length == 0)
            {
                return 0;
            }
            int min = int.MaxValue;
            foreach (int n in nums)
            {
                min = Math.Min(min, n);
            }
            int ans = 0;
            foreach (int n in nums)
            {
                ans = ans + n - min;
            }
            return ans;
        }
        #endregion
    }
}