using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace SampleDB
{
    public partial class DynamicScreen : ContentPage
    {
        public List<JSSEMasterBehavior> listOfObjects;
        public List<RatingTable> listOfRatings;
        public DynamicScreen()
        {
            InitializeComponent();



            var communicationTapGestureRecognizer = new TapGestureRecognizer();
            communicationTapGestureRecognizer.Tapped += (s, e) => {
                if (StackLayoutRef.IsVisible) {
                    StackLayoutRef.IsVisible = false;
                    arrowImageRef.Source = "ArrowDown";
                }else{
                    StackLayoutRef.IsVisible = true;
                    arrowImageRef.Source = "Arrowup";
                }
            };
            communicationRef.GestureRecognizers.Add(communicationTapGestureRecognizer);
            communicationTapGestureRecognizer.NumberOfTapsRequired = 1; 

		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await ReadDataFromJson();

            listOfRatings = await App.Database.GetRatings();
            listOfObjects = await App.Database.GetJsseBehaviors(0, 1);
            EnterpriseBehaviorsDesign(listOfObjects);

            OverAllRatingsDesign(listOfRatings, overAllratingButtionLayout);

		}

        public void EnterpriseBehaviorsDesign(List<JSSEMasterBehavior> listOfObjects) {


            for (int i = 0; i < listOfObjects.Count; i++){
                StackLayout firstStack = new StackLayout();
                firstStack.HorizontalOptions = LayoutOptions.FillAndExpand;
                firstStack.VerticalOptions = LayoutOptions.Start;
                if (i == 0)
                   firstStack.Margin = new Thickness(0, 20, 0, 0);

                StackLayout secondStack = new StackLayout();
                secondStack.Padding = new Thickness(0, 0, 20, 0);
                secondStack.Orientation = StackOrientation.Horizontal;
                secondStack.HorizontalOptions = LayoutOptions.FillAndExpand;
                secondStack.VerticalOptions = LayoutOptions.Start;
                secondStack.Spacing = 5;

				Label subtitle = new Label();
				subtitle.Margin = new Thickness(20, 0, 20, 0);
				subtitle.HorizontalOptions = LayoutOptions.FillAndExpand;
                subtitle.Text = listOfObjects[i].Behavior;
				subtitle.FontSize = 14;

                Button editButton = new Button();
                editButton.Image = "edit";
                editButton.HorizontalOptions = LayoutOptions.End;
                editButton.WidthRequest = 20;
                editButton.HeightRequest = 20;
                secondStack.Children.Add(subtitle);
                secondStack.Children.Add(editButton);
				firstStack.Children.Add(secondStack);

                StackLayout ratingsLayout = new StackLayout();
                ratingsLayout.Orientation = StackOrientation.Vertical;
                ratingsLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
                ratingsLayout.VerticalOptions = LayoutOptions.Start;
                ratingsLayout.Spacing = 5;

                BoxView topLine = new BoxView();
                topLine.HorizontalOptions = LayoutOptions.FillAndExpand;
                topLine.VerticalOptions = LayoutOptions.Start;
                topLine.HeightRequest = 1;
                topLine.BackgroundColor = Color.Silver;
                ratingsLayout.Children.Add(topLine);

                StackLayout subStackLayout = new StackLayout();
                subStackLayout.Spacing = 0;
                subStackLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
                subStackLayout.VerticalOptions = LayoutOptions.Start;
                subStackLayout.Orientation = StackOrientation.Horizontal;


                OverAllRatingsDesign(listOfRatings, subStackLayout);

    //            Button exceptionalBtn = new Button();
    //            exceptionalBtn.Text = "Exceptional";
    //            exceptionalBtn.HorizontalOptions = LayoutOptions.FillAndExpand;
    //            exceptionalBtn.BackgroundColor = Color.Transparent;
    //            subStackLayout.Children.Add(exceptionalBtn);

				//BoxView verticalLine = new BoxView();
    //            verticalLine.HorizontalOptions = LayoutOptions.Start;
    //            verticalLine.VerticalOptions = LayoutOptions.FillAndExpand;
    //            verticalLine.WidthRequest = 1;
				//verticalLine.BackgroundColor = Color.Silver;
    //            subStackLayout.Children.Add(verticalLine);


				//Button effectiveBtn = new Button();
				//effectiveBtn.Text = "Effective";
				//effectiveBtn.HorizontalOptions = LayoutOptions.FillAndExpand;
				//effectiveBtn.BackgroundColor = Color.Transparent;
				//subStackLayout.Children.Add(effectiveBtn);

				//BoxView verticalLinetwo = new BoxView();
				//verticalLinetwo.HorizontalOptions = LayoutOptions.Start;
				//verticalLinetwo.VerticalOptions = LayoutOptions.FillAndExpand;
				//verticalLinetwo.WidthRequest = 1;
				//verticalLinetwo.BackgroundColor = Color.Silver;
				//subStackLayout.Children.Add(verticalLinetwo);


				//Button needImpBtn = new Button();
				//needImpBtn.Text = "Effective";
				//needImpBtn.HorizontalOptions = LayoutOptions.FillAndExpand;
				//needImpBtn.BackgroundColor = Color.Transparent;
				//subStackLayout.Children.Add(needImpBtn);

                ratingsLayout.Children.Add(subStackLayout);


				BoxView bottomLine = new BoxView();
				bottomLine.HorizontalOptions = LayoutOptions.FillAndExpand;
				bottomLine.VerticalOptions = LayoutOptions.Start;
				bottomLine.HeightRequest = 1;
				bottomLine.BackgroundColor = Color.Silver;
				ratingsLayout.Children.Add(bottomLine);

                firstStack.Children.Add(ratingsLayout);




                StackLayoutRef.Children.Add(firstStack);


            }
        }

        public void OverAllRatingsDesign(List<RatingTable> listOfRatings, StackLayout layout){

            for (int i = 0; i < listOfRatings.Count; i++)
            {
                Button exceptionalBtn = new Button();
                exceptionalBtn.Text = listOfRatings[i].Rating;
                exceptionalBtn.HorizontalOptions = LayoutOptions.FillAndExpand;
                exceptionalBtn.BackgroundColor = Color.Transparent;
                layout.Children.Add(exceptionalBtn);
                exceptionalBtn.StyleId = listOfRatings[i].Rating_ID.ToString();

                if (i != listOfRatings.Count -1)
                {
                    BoxView verticalLine = new BoxView();
                    verticalLine.HorizontalOptions = LayoutOptions.Start;
                    verticalLine.VerticalOptions = LayoutOptions.FillAndExpand;
                    verticalLine.WidthRequest = 1;
                    verticalLine.BackgroundColor = Color.Silver;
                    layout.Children.Add(verticalLine);
                }
            }


        }

        public Task<int> ReadDataFromJson()
		{
			var assembly = typeof(DynamicScreen).GetTypeInfo().Assembly;
			Stream stream = assembly.GetManifestResourceStream("SampleDB.getbannerdata.json");

			using (var reader = new System.IO.StreamReader(stream))
			{

				var json = reader.ReadToEnd();
				List<GetActiveCategoriesModel.RootObject> data = JsonConvert.DeserializeObject<List<GetActiveCategoriesModel.RootObject>>(json);

				GetActiveCategoriesModel.RootObject[] arrayobj = data.ToArray();
				for (int k = 0; k < arrayobj.Length; k++)
				{
					JSSEMasterCategory mctbl = new JSSEMasterCategory();
					mctbl.Category_ID = arrayobj[k].Category_ID;
					mctbl.Category = arrayobj[k].Category;
					App.Database.SaveCategoriesAsync(mctbl);
					for (int t = 0; t < arrayobj[k].Ratings.Count; t++)
					{
						RatingTable rtbl = new RatingTable();
						rtbl.Rating_ID = arrayobj[k].Ratings[t].Rating_ID;
						rtbl.Rating = arrayobj[k].Ratings[t].Rating;
						App.Database.SaveRatingsAsync(rtbl);

					}
					GetActiveCategoriesModel.EntBehavior[] eorgdata = arrayobj[k].EntBehaviors.ToArray();
					for (int l = 0; l < eorgdata.Length; l++)
					{
						JSSEMasterBehavior mbhtbl = new JSSEMasterBehavior();

						mbhtbl.Behavior_ID = eorgdata[l].Behavior_ID;
						mbhtbl.Behavior = eorgdata[l].Behavior;
						mbhtbl.Category_ID = arrayobj[k].Category_ID;
						mbhtbl.BehaviorType_ID = eorgdata[l].BehaviorType_ID;
						App.Database.SaveBehaviorssAsync(mbhtbl);
					}

					for (int i = 0; i < arrayobj[k].AllOrgBehaviors.Count; i++)
					{
						for (int j = 0; j < arrayobj[k].AllOrgBehaviors[i].Count; j++)
						{
							JSSEMasterBehavior mbhtbl = new JSSEMasterBehavior();

							mbhtbl.Behavior_ID = arrayobj[k].AllOrgBehaviors[i][j].Behavior_ID;
							mbhtbl.Behavior = arrayobj[k].AllOrgBehaviors[i][j].Behavior;
							mbhtbl.Category_ID = arrayobj[k].Category_ID;
							mbhtbl.Org_ID = arrayobj[k].AllOrgBehaviors[i][j].Org_ID;
							mbhtbl.BehaviorType_ID = arrayobj[k].AllOrgBehaviors[i][j].BehaviorType_ID;
							App.Database.SaveBehaviorssAsync(mbhtbl);
						}
					}
				}

				

			}

            return Task.FromResult(1);
		}


     
	}
} 

