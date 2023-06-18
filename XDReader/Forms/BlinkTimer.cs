using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XDReader
{
    public partial class BlinkTimer : Form
    {
        public BlinkTimer(int[] timeline, int coolTime, int framesPerDelay = -1, int breakingFrames = 0, double frequency = 29.97 * 2, long baseTick = 0)
        {
            InitializeComponent();

            dataGridView1.Rows.Add(timeline.Length);
            for (int i = 0; i < timeline.Length; i++)
                dataGridView1.Rows[i].Cells[0].Value = timeline[i];

            checkPoints = new int[timeline.Length];
            checkPoints[0] = timeline[0];
            for (int i = 1; i < timeline.Length; i++)
                checkPoints[i] = checkPoints[i - 1] + timeline[i];

            this.frequency = frequency;
            this.breakingFrames = breakingFrames;

            blinkSoundPlayer = new SoundPlayer(Properties.Resources.button_7);
            beepPlayer = new SoundPlayer(Properties.Resources.beep_07a);

            this.baseTick = baseTick;
            _rollbackFrames = 10 + coolTime;
            _framesPerDelay = framesPerDelay;
        }

        private readonly SoundPlayer blinkSoundPlayer, beepPlayer;

        private readonly int _framesPerDelay;
        private readonly int _rollbackFrames;
        private readonly int breakingFrames;
        private readonly int beepCount = 5;
        private readonly double frequency;
        private readonly int[] checkPoints;

        private readonly long baseTick;

        // 瞬き開始までの開始時オフセット
        // 瞬き終了より早く切り上げる終了時オフセット
        // エンターキーバインド

        private int buffer = 0;
        private bool isWorking, isFinished = true;
        private async void button1_Click(object sender, EventArgs e)
        {
            if (isWorking || !isFinished)
            {
                isWorking = false;
                return;
            }

            button1.Text = "Stop";

            isWorking = true;
            isFinished = false;
            await Task.Run(() =>
            {
                MainLoop();

                isWorking = false;
                isFinished = true;
            });

            pictureBox1.Visible = true;
            button1.Text = "Start";
        }

        private void MainLoop()
        {
            var terminal = checkPoints.Last() - breakingFrames;
            var interval = 10_000_000 / frequency; // ticks per frames

            var rows = dataGridView1.Rows;
            using (var graphic = pictureBox1.CreateGraphics())
            {
                // タイマーのバーを初期化
                graphic.Clear(Color.Cyan);

                var start = DateTime.Now.Ticks;
                var nextFrame = start + interval;
                var nextDelay = start + interval * _framesPerDelay; // 一定フレームごとに遅延を考慮して遅らせる

                var nextBeep = beepCount - 1; // もしかしたらここも
                int frameCount = baseTick > 0 ?
                    (int)(start - baseTick).TickToFrame(frequency) : 0;

                int sequenceIndex = 0;
                // 二分探索にしたほうがいいかも
                for (; sequenceIndex < checkPoints.Length && checkPoints[sequenceIndex] < frameCount; sequenceIndex++) { }
                // 既にカウントが終了している場合
                if (sequenceIndex == checkPoints.Length) return;

                rows[sequenceIndex].Selected = true;
                for (; sequenceIndex < checkPoints.Length; sequenceIndex++)
                {
                    // タイマーの1区間あたりの処理
                    while (frameCount < checkPoints[sequenceIndex] + buffer)
                    {
                        var tick = DateTime.Now.Ticks;
                        if (tick >= nextFrame)
                        {
                            // 中断処理
                            if (!isWorking) return;

                            frameCount++;
                            nextFrame += interval;

                            // 残り時間の更新
                            var rem = terminal + buffer - frameCount;

                            // 終了間際の場合は遅延考慮処理をしない
                            // タイマーがバグるので…。
                            if (rem > 30 && _framesPerDelay != -1 && tick >= nextDelay)
                            {
                                UpdateBufferAsync(1);
                                nextDelay += interval * _framesPerDelay;
                            }

                            if (rem >= 0)
                                Task.Run(() => { try { Invoke((MethodInvoker)(() => UpdateTime(rem))); } catch { } });

                            // タイマーのバーの描画 ---

                            var beOverSoon = rem * 2 / frequency < beepCount - 1;

                            // 終了直前モードに入ったときにバーを赤色に変える処理
                            if (beOverSoon && (nextBeep == beepCount - 1)) 
                                graphic.Clear(Color.Red);

                            var barWidth = beOverSoon ? rem * 2.5f : (checkPoints[sequenceIndex] + buffer - frameCount) * 1.5f;

                            graphic.FillRectangle(Brushes.White, barWidth, 0, 300 - barWidth, 50);

                            // 終了直前は一定間隔で音を鳴らす
                            if (rem * 2 / frequency < nextBeep)
                            {
                                if (nextBeep >= 0)
                                    nextBeep--;
                                Task.Run(() => Invoke((MethodInvoker)beepPlayer.Play));
                            }
                        }
                    }

                    // 中断処理
                    if (!isWorking) return;

                    // 区間表示の更新
                    rows[sequenceIndex].Selected = false;
                    if (sequenceIndex < checkPoints.Length - 1)
                        rows[sequenceIndex + 1].Selected = true;

                    UpdateTimeline(sequenceIndex);

                    // 全体の待機時間終了まで余裕がある場合のみ
                    if ((terminal + buffer - frameCount) * 2 / frequency > beepCount)
                    {
                        // タイマーのバーを初期化
                        graphic.Clear(Color.Cyan);
                        // 区間の終わりに音を鳴らす処理
                        Task.Run(() => Invoke((MethodInvoker)blinkSoundPlayer.Play));
                    }
                    // カウント終了時にも1回鳴らす
                    else if (nextBeep == 0)
                    {
                        Task.Run(() => Invoke((MethodInvoker)beepPlayer.Play));
                    }
                }
            }
        }

        private Task UpdateTimeline(int i)
        {
            if (i < 4) return Task.CompletedTask;

            return Task.Run(() => Invoke((MethodInvoker)(() => dataGridView1.FirstDisplayedScrollingRowIndex = i - 4)));
        }


        private void UpdateTime(int frame) => label1.Text = $"{frame / frequency:f2}";

        private Task UpdateBufferAsync(int diff)
        {
            return Task.Run(() =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    UpdateBuffer(diff);
                }));
            });
        }
        private void UpdateBuffer(int diff)
        {
            buffer += diff;
            var sign = buffer == 0 ? "±" :
                buffer > 0 ? "+" : "";
            label2.Text = $"{sign}{buffer}";
        }
        private void button2_Click(object sender, EventArgs e) => UpdateBuffer(-5);
        private void button3_Click(object sender, EventArgs e) => UpdateBuffer(-1);
        private void button4_Click(object sender, EventArgs e) => UpdateBuffer(+1);
        private void button5_Click(object sender, EventArgs e) => UpdateBuffer(+5);
        private void button6_Click(object sender, EventArgs e) => UpdateBuffer(-_rollbackFrames);


        private void BlinkTimer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isWorking)
            {
                e.Cancel = true;
                isWorking = false;
                while (!isFinished) { }

                Close();
            }
        }


    }
}
