using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
using System.Text.Json;

namespace FinalProject
{
    public class SearchNovelViewModel : BaseViewModel
    {
        private string m_Keyword;
        public string SearchKeyword
        {
            get => m_Keyword;
            set
            {
                m_Keyword = value;
                OnPropertyChanged(nameof(SearchKeyword));
            }
        }
        private string m_selectedVolume;
        public string SelectedVolume
        {
            get => m_selectedVolume;
            set
            {
                m_selectedVolume = value;
                OnPropertyChanged(nameof(SelectedVolume));
            }
        }
        private Novel m_selectedNovel;
        public Novel SelectedNovel
        {
            get => m_selectedNovel;
            set
            {
                m_selectedNovel = value;
                OnPropertyChanged(nameof(SelectedNovel));
            }
        }
        public ObservableCollection<Novel> Novels { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand OpenDetailCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

       
        private NovelService m_novelSv;

        public SearchNovelViewModel()
        {
            m_novelSv = new NovelServiceWithFile();
            Novels = new ObservableCollection<Novel>(m_novelSv.SearchNovel(string.Empty, string.Empty));
            SearchCommand = new ConditionalCommand(x => DoSearch());
            ResetCommand = new ConditionalCommand(x => DoReset());
            OpenDetailCommand = new ConditionalCommand(x => DoOpenDetail());
            DeleteCommand = new ConditionalCommand(x => DoDelete());

        }
        public void DoSearch()
        {
            Novels.Clear();
            var result = m_novelSv.SearchNovel(SearchKeyword, SelectedVolume);
            foreach (var s in result)
            {
                Novels.Add(s);
            }
        }
        private void DoReset()
        {
            SearchKeyword = null;
            SelectedVolume = null;
        }
        private void DoDelete()
        {
            m_novelSv.DeleteById(SelectedNovel.NovelId);
            DoSearch();
        }
        public void DoOpenDetail()
        {
            var detailviewmodel = new NovelDetailViewModel(m_novelSv, SelectedNovel.NovelId);
            NovelDetailWindow novelDetail = new NovelDetailWindow(detailviewmodel);
            novelDetail.DataContext = detailviewmodel;
            novelDetail.ShowDialog();
        }

    }
}
