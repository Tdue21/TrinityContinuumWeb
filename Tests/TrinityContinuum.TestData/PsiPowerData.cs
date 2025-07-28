using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrinityContinuum.TestData;
public static class PsiPowerData
{
    public static string PsiPowersJson => """
    [
        {
            "Aptitude": "Biokinesis",
            "Mode": "Adaptation",
            "Name": "Resist",
            "Description": "The psion mitigates sources of harm coming from within her body.",
            "System": "Success reduces the damage rating of hazards such as toxins, irritants, and diseases within the psion's body by an amount equal to her Mode dots. If the power reduces the damage rating to zero the character can either internally neutralize the substance, or expel it intact from her body. If the damage rating of a hazard is not reduced to zero, it continues to damage the psion at the reduced rate.",
            "Dots": 1,
            "Cost": "0",
            "Source": "ÆON",
            "Page": 211
        },
        {
            "Aptitude": "Biokinesis",
            "Mode": "Adaptation",
            "Name": "Acclimatize",
            "Description": "A psion can adjust his physiology to best suit the environment.",
            "System": "The psion suffers no penalties from increased or decreased gravity that is less than extreme gravity and can also ignore all environmental indirect damage with the Continuous (hour) tag as well as all environmental indirect damage with the Continuous (minute) tag that does not also possess the Aggravated tag. The psion can reduce the damage rating of other forms of environmental indirect damage by one per success spent.",
            "Dots": 2,
            "Cost": "0",
            "Source": "ÆON",
            "Page": 211
        },
        {
            "Aptitude": "Biokinesis",
            "Mode": "Adaptation",
            "Name": "Metabolic Control",
            "Description": "The biokinetic controls the speed of her body's processes. While this can reduce harmful conditions, it also works to repair damage.",
            "System": "If successful, the character changes the rate of one aspect of her physiology. For functions that take a defined time, such as healing, holding her breath, or going without water, she multiplies (or divides) the time by Mode dots + successes. For processes not measured in time, such as Initiative, the character either adds (or subtracts) Mode dots/2 to her total.",
            "Dots": 3,
            "Cost": "1",
            "Source": "ÆON",
            "Page": 211
        },
        {
            "Aptitude": "Biokinesis",
            "Mode": "Adaptation",
            "Name": "Resist",
            "Description": "The psion mitigates sources of harm coming from within her body.",
            "System": "Success reduces the damage rating of hazards such as toxins, irritants, and diseases within the psion's body by an amount equal to her Mode dots. If the power reduces the damage rating to zero the character can either internally neutralize the substance, or expel it intact from her body. If the damage rating of a hazard is not reduced to zero, it continues to damage the psion at the reduced rate.",
            "Dots": 1,
            "Cost": "0",
            "Source": "ÆON",
            "Page": 211
        },
        {
            "Aptitude": "Biokinesis",
            "Mode": "Adaptation",
            "Name": "Acclimatize",
            "Description": "A psion can adjust his physiology to best suit the environment.",
            "System": "The psion suffers no penalties from increased or decreased gravity that is less than extreme gravity and can also ignore all environmental indirect damage with the Continuous (hour) tag as well as all environmental indirect damage with the Continuous (minute) tag that does not also possess the Aggravated tag. The psion can reduce the damage rating of other forms of environmental indirect damage by one per success spent.",
            "Dots": 2,
            "Cost": "0",
            "Source": "ÆON",
            "Page": 211
        },
        {
            "Aptitude": "Biokinesis",
            "Mode": "Adaptation",
            "Name": "Metabolic Control",
            "Description": "The biokinetic controls the speed of her body's processes. While this can reduce harmful conditions, it also works to repair damage.",
            "System": "If successful, the character changes the rate of one aspect of her physiology. For functions that take a defined time, such as healing, holding her breath, or going without water, she multiplies (or divides) the time by Mode dots + successes. For processes not measured in time, such as Initiative, the character either adds (or subtracts) Mode dots/2 to her total.",
            "Dots": 3,
            "Cost": "1",
            "Source": "ÆON",
            "Page": 211
        }
    ]
    """;

}
