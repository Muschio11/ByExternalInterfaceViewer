namespace ByExternalInterfaceViewer.Models.ExternalinterfaceDBModels;

public class MovementsListModel
{
    public long OperationID { get; set; }
    public DateTime OperationTime { get; set; }
    public string MaterialName { get; set; }
    public string MaterialDescription { get; set; }
    public double Lenght { get; set; }
    public double Width { get; set; }
    public double Thickness { get; set; }
    public int Quantity { get; set; }
    public string? SheetType { get; set; }
    public string? CuttingPlan { get; set; }
    public string? Description { get; set; }
    public string? DocumentName { get; set; }
    public string? SupplierName { get; set; }
    public int CassetteID { get; set; }
    public string? Status { get; set; }
    public int LocationID { get; set; }
    public string? additionalField1 { get; set; }
    public string? additionalField2 { get; set; }
    public string? additionalField3 { get; set; }
    public string? additionalField4 { get; set; }
    public string? additionalField5 { get; set; }
    public string? additionalField6 { get; set; }
    public string? additionalField7 { get; set; }
    public string? additionalField8 { get; set; }
    public string? additionalField9 { get; set; }
    public string? additionalField10 { get; set; }
    public string? Icon { get; set; }
    public long SheetGUID { get; set; }
    public short Elab { get; set; }
    public DateTime DataElab { get; set; }
}
