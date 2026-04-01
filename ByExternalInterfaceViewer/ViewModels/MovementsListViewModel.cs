using ByExternalInterfaceViewer.Models.Database3DBModels;
using ByExternalInterfaceViewer.Models.ExternalinterfaceDBModels;
using ByExternalInterfaceViewer.Services.Database3DB;
using ByExternalInterfaceViewer.Services.ExternalInterfaceDB;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ByExternalInterfaceViewer.ViewModels;

public partial class MovementsListViewModel: ObservableObject
{
   private readonly IDbContextFactory<AppDbContextExternalInterface> _dbContextFactoryExtInt;
    private readonly IDbContextFactory<AppDBContextDatabase3> _dbContextFactoryDb3;

    [ObservableProperty]
    private ObservableCollection<MovementsListModel> _movements = new ();
    
    

    public MovementsListViewModel(IDbContextFactory<AppDbContextExternalInterface> dbContextFactoryExtInt, IDbContextFactory<AppDBContextDatabase3> dbContextFactoryDb3)
    {
        _dbContextFactoryExtInt = dbContextFactoryExtInt;
        _dbContextFactoryDb3 = dbContextFactoryDb3;

        _ =GetMovementsAsync();
    }


    public async Task GetMovementsAsync()
    {
        try
        {
            await using var contextExtInt = await _dbContextFactoryExtInt.CreateDbContextAsync();
            await using var contextDb3 = await _dbContextFactoryDb3.CreateDbContextAsync();

            //var movements = await context.Locations.AsNoTracking().ToListAsync();
            Movements.Clear();
            
            //Query of single Database to perform join
            var queryMovementList = await contextExtInt.MovementsList.OrderByDescending(m=>m.OperationID).Take(100).AsNoTracking().ToListAsync();
            var queryLocations = await contextDb3.Locations.Where(l=>l.LocationID != 0 && l.LocationType != "magazzino").AsNoTracking().ToListAsync();

            var query = queryMovementList.Join(queryLocations,m=> m.LocationID, l => l.LocationID, (m, l) => new MovementsListModel
            {
                OperationID = m.OperationID,
                OperationTime = m.OperationTime,
                MaterialName = m.MaterialName,
                MaterialDescription = m.MaterialDescription,
                Length = m.Length,
                Width = m.Width,
                Thickness = m.Thickness,
                Quantity = m.Quantity,
                SheetType = m.SheetType,
                CuttingPlan = m.CuttingPlan,
                Description = m.Description,
                DocumentName = m.DocumentName,
                SupplierName = m.SupplierName,
                CassetteID = m.CassetteID,
                Status = m.Status,
                LocationID = m.LocationID,
                LocationName = l.LocationType,
                AdditionalField1 = m.AdditionalField1,
                AdditionalField2 = m.AdditionalField2,
                AdditionalField3 = m.AdditionalField3,
                AdditionalField4 = m.AdditionalField4,
                AdditionalField5 = m.AdditionalField5,
                AdditionalField6 = m.AdditionalField6,
                AdditionalField7 = m.AdditionalField7,
                AdditionalField8 = m.AdditionalField8,
                AdditionalField9 = m.AdditionalField9,
                AdditionalField10 = m.AdditionalField10,
                Icon = m.Icon,
                SheetGUID = m.SheetGUID,
                Elab = m.Elab,
                DateElab = m.DateElab
            }).ToList();
            

            Movements = new ObservableCollection<MovementsListModel>(query);


            


        }
        catch (Exception ex)
        {
           
        }
    }
}
