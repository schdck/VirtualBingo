using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using SimpleDialogs;
using SimpleDialogs.Controls;
using SimpleDialogs.Enumerators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using VirtualBingo.Helpers;
using VirtualBingo.UI.Player.Classes;
using VirtualBingo.UI.Player.Views.Controls;
using VirtualBingo.UI.Shared.Models;

namespace VirtualBingo.UI.Player.ViewModels
{
    public class GenerateCardsViewModel : ViewModelBase
    {
        public ICommand GenerateCardsCommand { get; private set; }

        public int? AmountOfCards { get; set; }
        public int? AmountOfQuestionsPerCard { get; set; }

        public GenerateCardsViewModel()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            GenerateCardsCommand = new RelayCommand<GameInfo>((x) =>
            {
                if (x == null || AmountOfCards == null || AmountOfQuestionsPerCard == null)
                {
                    DialogManager.ShowDialog(new AlertDialog()
                    {
                        AlertLevel = AlertLevel.Error,
                        Title = Properties.Resources.GENERIC_Error,
                        Message = Properties.Resources.GENERIC_FillNecessaryFields
                    });

                    return;
                }

                BingoGame game;

                try
                {
                    game = BingoGame.LoadGame(x.Language, x.Subject, x.Topic);
                }
                catch (Exception e)
                {
                    DialogManager.ShowDialog(new AlertDialog()
                    {
                        AlertLevel = AlertLevel.Error,
                        Title = Properties.Resources.GENERIC_Error,
                        Message = Properties.Resources.GENERATE_CARDS_ErrorLoadingGame,
                        Exception = e,
                        ShowCopyToClipboardButton = true,

                    });

                    return;
                }

                AutoResetEvent autoReset = new AutoResetEvent(false);
                IEnumerable<BingoCard> cards = null;

                SaveFileDialog saveDialog = new SaveFileDialog()
                {
                    Filter = "PDF (*.pdf)|*.pdf",
                };

                PdfDocument pdf = new PdfDocument();
                pdf.Info.Author = "VirtualBingo";
                pdf.Info.Title = game.Topic;

                Thread thread = new Thread(() =>
                {
                    cards = game.DistributeQuestions(AmountOfCards.Value, AmountOfQuestionsPerCard.Value);

                    string tempImagePath = Path.GetTempFileName();

                    foreach (var card in cards)
                    {
                        PdfPage page = pdf.AddPage();

                        page.Orientation = PdfSharp.PageOrientation.Landscape;
                        page.Size = PdfSharp.PageSize.A4;

                        CardDisplayer displayer = new CardDisplayer()
                        {
                            Card = card
                        };

                        RendererHelper.SaveImageOfRender(RendererHelper.Render(displayer, new Size(842, 595)), tempImagePath);

                        using (FileStream stream = File.Open(tempImagePath, FileMode.Open, FileAccess.Read, FileShare.Delete))
                        {
                            using (XGraphics gfx = XGraphics.FromPdfPage(page, XGraphicsUnit.Millimeter))
                            {
                                using (XImage xImage = XImage.FromStream(stream))
                                {
                                    gfx.DrawImage(xImage, 15, 15, 267, 180);
                                }
                            }
                        }
                    }
                });

                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();

                if (saveDialog.ShowDialog() == true)
                {
                    thread.Join();

                    pdf.Save(saveDialog.FileName);

                    DialogManager.ShowDialog(new AlertDialog()
                    {
                        Title = Properties.Resources.GENERIC_Success,
                        Message = Properties.Resources.GENERATE_CARDS_SuccessGenerating
                    });
                }
            });
        }
    }
}
