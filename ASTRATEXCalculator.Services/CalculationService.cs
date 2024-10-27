using System.Linq;
using ASTRATEXCalculator.Common.Dtos;
using ASTRATEXCalculator.Common.Enums;
using ASTRATEXCalculator.Common.Filters;
using ASTRATEXCalculator.Common.Models;
using ASTRATEXCalculator.Data.Converters;
using ASTRATEXCalculator.Data.Entities;
using ASTRATEXCalculator.Data.Repositories.Interfaces;
using ASTRATEXCalculator.Services.Abstractions;
using Calculation = ASTRATEXCalculator.Common.Dtos.Calculation;

namespace ASTRATEXCalculator.Services
{
    public class CalculationService(IRepositoryManager repositoryManager, ConvertingManager convertingManager) : ICalculationService
    {
        
        private const int NumberOfRecentCalculations = 10;
        

        public void AddCalculation(Calculation calculation)
        {
            var dbCalculation = new Data.Entities.Calculation
            {
                FirstNumber = calculation.FirstNumber,
                Operation = calculation.Operation,
                SecondNumber = calculation.SecondNumber,
                Result = calculation.Result,
                Created = calculation.Created,
                Status = EntityStatus.Active
            };

            repositoryManager.Calculations.Add(dbCalculation);
            repositoryManager.Calculations.SaveChanges();
        }

        public ItemsContainer<Calculation> GetRecentCalculations()
        {
            var (calculations, total) = repositoryManager.Calculations.List(new CalculationFilter
            {
                Take = NumberOfRecentCalculations,
                Status = EntityStatus.Active
            });

            var calculationDtos = calculations.Select(x => convertingManager.Calculations.EntityToDto(x)).OrderByDescending(x => x.Created);

            return new ItemsContainer<Calculation>(calculationDtos, total);
        }
    }
}