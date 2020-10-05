using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using UL.Calculator.Validators;

namespace UL.Calculator.Services.Dependencies
{
    public static class ServiceConfigurationManager
    {
        public static void ConfigureServiceLifeTime(IServiceCollection services)
        {
            services.AddTransient<ICalculatorService, CalculatorService>();
            services.AddTransient<IExpressionValidator, ExpressionValidator>();
            services.AddTransient<IExpressionCalculator, ExpressionCalculator>();
        }
    }
}