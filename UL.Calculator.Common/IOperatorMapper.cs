using System.Collections.Generic;

namespace UL.Calculator.Common
{
    public interface IOperatorMapper
    {
        Dictionary<char, OperatorBase> GetMapping();
    }
}