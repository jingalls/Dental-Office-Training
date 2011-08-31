using System;
using System.Collections.Generic;
using App_Code;

/// <summary>
/// 
/// </summary>
public partial class StudentProfiles : System.Web.UI.Page
{
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadPanes();
        }
    }

    /// <summary>
    /// Loads the panes.
    /// </summary>
    private void LoadPanes()
    {
        List<DOTClass> classes = Helper.LoadDOTClasses();

        foreach (DOTClass dotClass in classes)
        {
            AjaxControlToolkit.AccordionPane pane = new AjaxControlToolkit.AccordionPane { Header = new AccordionTemplateBuilder("<div>" + dotClass.Period + "</div>") };
            pane.ID = "acdnpn" + dotClass.Period;
            pane.ContentContainer.Controls.Add(Helper.BuildClassHeaderControl(dotClass));
            pane.ContentContainer.Controls.Add(Helper.BuildClassContentControl(dotClass));

            Accordion1.Panes.Add(pane);
        }
    }
}
