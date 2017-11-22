using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace SampleDB
{
    public partial class PickerPage : ContentPage
    {
        public string orgid;
        public int Org_ID;
        List<int> orgIdsList;
        List<String> orgNamesList;
        public PickerPage()
        {
            InitializeComponent();
            orgIdsList = new List<int>();
            orgNamesList = new List<string>();
			var assembly = typeof(DynamicScreen).GetTypeInfo().Assembly;
			Stream stream = assembly.GetManifestResourceStream("SampleDB.organizations.json");

			using (var reader = new System.IO.StreamReader(stream))
			{

				var json = reader.ReadToEnd();
                List<GetOrganizationsModel.RootObject> data = JsonConvert.DeserializeObject<List<GetOrganizationsModel.RootObject>>(json);
				GetOrganizationsModel.RootObject[] arrayobj = data.ToArray();
                if (orgNamesList.Count > 0){
                    orgNamesList.Clear();
                }
				for (int k = 0; k < arrayobj.Length; k++)
				{
                    GetOrganizationsModel.Organization[] eorgdata = arrayobj[k].Organizations.ToArray();
                    for (int l = 0; l < eorgdata.Length; l++)					
                    {
                        //orgpicker.Items.Add(eorgdata[l].Org_Name);
                        orgNamesList.Add(eorgdata[l].Org_Name);
                        orgIdsList.Add(int.Parse(eorgdata[l].Org_Id));
					}
				}

                orgpicker.ItemsSource = orgNamesList;
			}

            orgpicker.SelectedIndexChanged += (sender, args) =>
             {
                 orgid = orgpicker.SelectedItem.ToString();
                 Org_ID = orgIdsList[((Picker)sender).SelectedIndex];

             };

		    submitRef.Clicked += (object sender, EventArgs e) =>
              {
                  if (Org_ID > 0)
                  {
                    Navigation.PushAsync(new CommunicationsPage(Org_ID));
                  }

              };
		}
    }
}
