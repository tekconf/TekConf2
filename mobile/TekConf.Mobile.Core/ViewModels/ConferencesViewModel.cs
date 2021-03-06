using MvvmCross.Core.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AutoMapper;
using System.Threading.Tasks;
using PropertyChanged;

namespace TekConf.Mobile.Core.ViewModels
{
	[ImplementPropertyChanged]
	public class ConferencesViewModel : MvxViewModel
	{
		readonly IConferencesService _conferencesService;
		readonly IMapper _mapper;

		public ObservableCollection<ConferenceListViewModel> Conferences { get; set; } = new ObservableCollection<ConferenceListViewModel>();
		public bool IsLoading { get; set; }

		public ConferencesViewModel(IConferencesService conferencesService, IMapper mapper)
		{
			_mapper = mapper;
			_conferencesService = conferencesService;
		}

		private ICommand _loadCommand;
		public ICommand LoadCommand
		{
			get
			{
				_loadCommand = _loadCommand ?? new MvxAsyncCommand(Load, CanLoad);
				return _loadCommand;
			}
		}

		private async Task Load()
		{
			IsLoading = true;
			var conferenceModels = await _conferencesService.Load();
			var conferenceViewModels = _mapper.Map<IList<ConferenceListViewModel>>(conferenceModels);
			this.Conferences = new ObservableCollection<ConferenceListViewModel>(conferenceViewModels);
			IsLoading = false;
		}

		bool CanLoad()
		{
			return true;
		}

	}
}
