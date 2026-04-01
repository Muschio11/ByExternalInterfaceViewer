using System.ComponentModel.DataAnnotations.Schema;

namespace ByExternalInterfaceViewer.Models.ExternalinterfaceDBModels;

public class MovementsListModel
{
    public long OperationID { get; set; }
    public DateTime OperationTime { get; set; }
    public string MaterialName { get; set; }
    public string MaterialDescription { get; set; }
    public double Length { get; set; }
    public double Width { get; set; }
    public double Thickness { get; set; }
    public int Quantity { get; set; }
    public string SheetType { get; set; }
    public string? CuttingPlan { get; set; }
    public string? Description { get; set; }
    public string? DocumentName { get; set; }
    public string? SupplierName { get; set; }
    public int CassetteID { get; set; }
    public string? Status { get; set; }
    public int LocationID { get; set; }
    [NotMapped]
    public string LocationName { get; set; }
    public string? AdditionalField1 { get; set; }
    public string? AdditionalField2 { get; set; }
    public string? AdditionalField3 { get; set; }
    public string? AdditionalField4 { get; set; }
    public string? AdditionalField5 { get; set; }
    public string? AdditionalField6 { get; set; }
    public string? AdditionalField7 { get; set; }
    public string? AdditionalField8 { get; set; }
    public string? AdditionalField9 { get; set; }
    public string? AdditionalField10 { get; set; }
    public string? Icon { get; set; }
    public long? SheetGUID { get; set; }
    public short? Elab { get; set; }
    public DateTime? DateElab { get; set; }
}
