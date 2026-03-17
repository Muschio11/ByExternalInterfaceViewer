using ByExternalInterfaceViewer.Models.ExternalinterfaceDBModels;
using ByExternalInterfaceViewer.Services.ExternalInterfaceDB;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ByExternalInterfaceViewer.ViewModels
{
    public partial class CassetteContentsViewModel : ObservableObject
    {
        private readonly IDbContextFactory<AppDbContextExternalInterface> _dbContextFactory;

        [ObservableProperty]
        private ObservableCollection<CassetteContentsListModel> _cassetteContents = new();

        public CassetteContentsViewModel(IDbContextFactory<AppDbContextExternalInterface> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _ = GetCassetteContentsAsync();
        }

        public async Task GetCassetteContentsAsync()
        {
            try
            {
                await using var context = await _dbContextFactory.CreateDbContextAsync();

                CassetteContents.Clear();
                var query = await context.CassetteContentsList.AsNoTracking().ToListAsync();
                CassetteContents = new ObservableCollection<CassetteContentsListModel>(query);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
