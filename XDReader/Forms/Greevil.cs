using PokemonPRNG.LCG32;
using PokemonPRNG.LCG32.GCLCG;
using PokemonStandardLibrary;
using PokemonStandardLibrary.CommonExtension;
using PokemonXDRNGLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XDReader.Forms
{
    public partial class Greevil : Form
    {
        public Greevil()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = comboBox2.SelectedIndex = 12;
            rhydonAbility.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var hpIsEven = new bool?[]
            {
                hpEven0.Checked,
                hpEven1.Checked,
                useExeggutor.Checked ? (bool?)hpEven2.Checked : null,
                useTauros.Checked ? (bool?)hpEven3.Checked : null,
                useArticuno.Checked ? (bool?)hpEven4.Checked : null,
                useZapdos.Checked ? (bool?)hpEven5.Checked : null,
            };
            var rhydonGender = male0.Checked ? Gender.Male : Gender.Female;
            var exeggutorGender = male2.Checked ? Gender.Male : Gender.Female;
            var lightningRod = rhydonAbility.SelectedIndex > 0 ? (bool?)(rhydonAbility.SelectedIndex == 2) : null;

            var seed = currentSeedBox.Seed;
            var min = (int)minFrameBox.Value;
            var max = (uint)maxFrameBox.Value;
            if (max < min) return;

            var range = (int)(max - min + 1);
            var offset = min;
            if (min < 0)
                seed.Back((uint)(-min));
            else
                seed.Advance((uint)min);

            var generator = new TeamGenerator(
                TeamGenerator.Greevil, 
                new FirstCameraAngleGenerator(
                    (uint)comboBox1.SelectedIndex, 
                    (uint)comboBox2.SelectedIndex
                )
            );
            var criteria = new GreevilCriteria(
                hpIsEven: hpIsEven, 
                rhydonGender: rhydonGender, 
                exeggutorGender: exeggutorGender, 
                lightningRod: lightningRod
            );

            var result = new StringBuilder();
            foreach(var (i, team) in seed.EnumerateSeed().EnumerateGeneration(generator).Take(range).WithIndex(offset).Where(_ => criteria.Check(_.Element)))
            {
                result.AppendLine($"{i}[F]");
                foreach (var p in team)
                    result.AppendLine($"\t{p.Name} HP:{p.Stats[0]} Gender:{p.Gender.ToSymbol()} Ability:{p.GCAbility}");
            }
            
            textBox1.Text = result.ToString();
        }
    }

    class GreevilCriteria
    {
        private readonly bool?[] hpIsEven;
        private readonly Gender rhydonGender;
        private readonly Gender exeggutorGender;
        private readonly bool? lightningRod;

        public GreevilCriteria(bool?[] hpIsEven, Gender rhydonGender, Gender exeggutorGender, bool? lightningRod)
        {
            this.hpIsEven = hpIsEven;
            this.rhydonGender = rhydonGender;
            this.exeggutorGender = exeggutorGender;
            this.lightningRod = lightningRod;
        }

        public bool Check(GCIndividual[] team)
        {
            for (int i=0; i<team.Length; i++)
            {
                if (hpIsEven[i] != null && team[i].Stats[0] % 2 == 0 != hpIsEven[i]) return false;
            }

            if (team[0].Gender != rhydonGender) return false;
            if (hpIsEven[2] != null && team[2].Gender != exeggutorGender) return false;
            if (lightningRod != null && team[0].GCAbility == "ひらいしん" != lightningRod) return false;

            return true;
        }
    }
}
