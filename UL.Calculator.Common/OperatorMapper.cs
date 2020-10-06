using System.Collections.Generic;
using UL.Calculator.Common.Enums;

namespace UL.Calculator.Common
{
    public class OperatorMapper : IOperatorMapper
    {
        public Dictionary<char, OperatorBase> GetMapping()
        {
            return new Dictionary<char, OperatorBase>
            {
                {'+', new OperatorBase {Priority = OperatorPriority.Addition}},
                {'-', new OperatorBase {Priority = OperatorPriority.Subtraction}},
                {'/', new OperatorBase {Priority = OperatorPriority.Division}},
                {'*', new OperatorBase {Priority = OperatorPriority.Multiplication}}
            };
        }
    }
}