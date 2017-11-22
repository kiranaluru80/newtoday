using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SampleDB
{
    public partial class EmployeePage : ContentPage
    {
        public EmployeePage()
        {
            InitializeComponent();
        }
         async void Saved_Clicked(object sender, System.EventArgs e)
        {
            var personitem = (Employee)BindingContext;
            //for (int i = 0; i < 1000; i++)
            //{
            //    personitem.EmpName = "ravi" + i;
            //    personitem.Designation = "ravi" + i;
            //    personitem.Age = 50 + i;
            //    personitem.EmpId = 50 + i;
            //    await App.Database.SaveEmployeeAsync(personitem);
            //}
			await App.Database.SaveEmployeeAsync(personitem);

			await Navigation.PopAsync();
        }
        async void Cancel_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}
