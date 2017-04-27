using ElevenNote.MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ElevenNote.MobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotesPage : ContentPage
	{

        private List<NoteListItemViewModel> Notes { get; set; }

        public NotesPage()
        {
            InitializeComponent();
            SetupUi();
        }

        private async Task PopulateNotesList()
        {
            await App.NoteService
                .GetAll()
                .ContinueWith(task =>
            {
                var notes = task.Result;

                Notes = notes
                    .OrderByDescending(note => note.IsStarred) //descending because 1 is greater than 0, and true == 1
                    .ThenByDescending(note => note.CreatedUtc) //show newest notes first
                    .Select(s => new NoteListItemViewModel
                    {
                        NoteId = s.NoteId,
                        Title = s.Title,
                        StarImage = s.IsStarred ? "starred.png" : "notstarred.png"
                    })
                        .ToList();

                    lvwNotes.ItemsSource = Notes;

                    // Clear any item selection.
                    lvwNotes.SelectedItem = null;

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void SetupUi()
        {
            //Wire up refereshing
            lvwNotes.IsPullToRefreshEnabled = true;
            lvwNotes.Refreshing += async (o, args) =>
            {
                await PopulateNotesList();
                lvwNotes.IsRefreshing = false;
                lblNoNotes.IsVisible = !Notes.Any();
            };

            //Add "New Note" is to title bar.
            this.ToolbarItems.Add(new ToolbarItem("Add", null, async () =>
            {
                await Navigation.PushAsync(new NoteDetailPage(null));
            }));

            this.ToolbarItems.Add(new ToolbarItem("Log Out", null, async () =>
            {
                if (await DisplayAlert("Well?", "Are you sure you want to quit back to the login screen?", "Yep", "Nope"))
                {
                    await Navigation.PopAsync(true);
                }
            }));

        }

        //Whenever the view appears, updates the notes list.
        protected override async void OnAppearing()
        {
            await PopulateNotesList();
        }

        private void LvwNotes_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Load the note detail page.
            if (e.SelectedItem != null)
            {
                var note = e.SelectedItem as NoteListItemViewModel;
                Navigation.PushAsync(new NoteDetailPage(note.NoteId));
            }
        }
    }
}
