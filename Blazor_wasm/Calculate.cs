using Blazor_wasm.Controller;
using Blazor_wasm.Models.DatabaseModels;
using Microsoft.AspNetCore.Components;

namespace Blazor_wasm
{
    public class Calculate<T>
    {
        public (int, double) LifeSpanCauculate(T secondLargesItem, double currentZero, double currentSlope, int a, int b, int c, double fieldDecline, int constantFieldTotalDays)
        {
            var secondLargesItemTemp = secondLargesItem as CalDataModel;

            var absCurrentZero = Math.Abs(currentZero);
            var absSecondZero = Math.Abs(secondLargesItemTemp!.Zero);

            var absSlopeDifferenceBetweenCurrentAndStandard = Math.Abs(currentSlope + 59.16);
            var absSlopeDifferenceBetweenSecondLargestAndStandard = Math.Abs(secondLargesItemTemp.Slope + 59.16);

            var zeroCalculate = Math.Abs(currentZero - secondLargesItemTemp.Zero) * 8;
            var slopeCalculate = Math.Abs(currentSlope - secondLargesItemTemp.Slope)/ 59.16 * 100 * 30;

            var zeroGainPercentage = absCurrentZero > absSecondZero ? -(zeroCalculate) : zeroCalculate;
            var slopeGainPercentage = absSlopeDifferenceBetweenCurrentAndStandard > absSlopeDifferenceBetweenSecondLargestAndStandard ? -(slopeCalculate) : slopeCalculate;

            var totalGainPercentage_d = zeroGainPercentage + slopeGainPercentage;

            var addOrSubtractDaysAfterCal_e = totalGainPercentage_d * b;

            var feedbackDays_f = (int)((b + addOrSubtractDaysAfterCal_e) * fieldDecline);    

            var actualRemainingDays_g = c + feedbackDays_f;

            var endurancePercentage = (actualRemainingDays_g / constantFieldTotalDays) * 100.0;

            return (actualRemainingDays_g, endurancePercentage);

        }
    }
}
