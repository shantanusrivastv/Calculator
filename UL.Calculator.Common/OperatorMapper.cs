using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace UL.Calculator.Common
{
    public class OperatorMapper
    {

        public Dictionary<char, OperatorBase> GetMapping()
        {
            return new Dictionary<char, OperatorBase>()
            {
                {'+', new OperatorBase() {Priority = OperatorPriority.Addition}},
                {'-', new OperatorBase() {Priority = OperatorPriority.Subtraction}},
                {'/', new OperatorBase() {Priority = OperatorPriority.Division}},
                {'*', new OperatorBase() {Priority = OperatorPriority.Multiplication}}
            };
        }
    }
}
