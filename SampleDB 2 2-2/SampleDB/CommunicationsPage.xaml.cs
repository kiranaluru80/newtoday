using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using static SampleDB.SaveJsseModel;

namespace SampleDB
{
    public partial class CommunicationsPage : ContentPage
    {
        

        public List<JSSEMasterCategory> listOfCategarys;
		public List<RatingTable> listOfRatings;
        public List<JSSEMasterBehavior> listOfEnterprisePerCategary;
        public List<JSSEMasterBehavior> listOfBehaviersPerCategary;

        public List<Categories> InputCategories;
        public List<EntBehavior> InputOrgBehaviors;

        public int _Org_Id;
        public static Editor SelectedEditor;

        public CommunicationsPage(int Org_Id)
        {
            InitializeComponent();

            saveBtnRef.Clicked += SaveBtnRef_Clicked;

            InputCategories = new List<Categories>() ;
            InputOrgBehaviors = new List<EntBehavior>();
            _Org_Id = Org_Id;


            ClearButtonRef.Clicked += (sender, e) => {
                if (SelectedEditor != null)
                {
                    SelectedEditor.Text = "";
                    CommentsEditorref.Text = "";
                }
            };

            ContinueButtonref.Clicked += (sender, e) => {
                if(SelectedEditor != null){
                    CommentsView.IsVisible = false;
                    SelectedEditor.Text = CommentsEditorref.Text;
                }
            };

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

           // listOfObjects = await App.Database.GetJsseBehaviors(0,1);
			await ReadDataFromJson();

            listOfCategarys = await App.Database.GetJsseCategories();
            listOfRatings = await App.Database.GetRatings();
           // listOfObjectsPerCategary = await App.Database.GetJsseBehaviors(0, 1);
            DynamicCategaryDesign(listOfCategarys);

        }


        public async void DynamicCategaryDesign(List<JSSEMasterCategory> listOfCategarys){


            for (int i = 0; i < listOfCategarys.Count; i++)
            {
                StackLayout mainStackLayout = new StackLayout();
                mainStackLayout.Padding = new Thickness(20, 0, 20, 0);
                mainStackLayout.Orientation = StackOrientation.Horizontal;
                mainStackLayout.Spacing = 20;
                mainStackLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
                mainStackLayout.VerticalOptions = LayoutOptions.Start;
                mainStackLayout.BindingContext = listOfCategarys[i];
				
				Label titleLabel = new Label();
                titleLabel.Text = listOfCategarys[i].Category;
                titleLabel.HorizontalOptions = LayoutOptions.FillAndExpand;
                titleLabel.VerticalOptions = LayoutOptions.Center;
                titleLabel.FontSize = 16;
                titleLabel.TextColor = Color.Black;
                titleLabel.FontAttributes = FontAttributes.Bold;

                Image arrowImage = new Image();
                arrowImage.Source = "ArrowDown";
                arrowImage.HorizontalOptions = LayoutOptions.End;
                arrowImage.VerticalOptions = LayoutOptions.Center;
                arrowImage.HeightRequest = 25;
                arrowImage.WidthRequest = 25;

                mainStackLayout.Children.Add(titleLabel);
                mainStackLayout.Children.Add(arrowImage);



                BoxView bottomLine = new BoxView();
                bottomLine.HorizontalOptions = LayoutOptions.FillAndExpand;
                bottomLine.VerticalOptions = LayoutOptions.Start;
                bottomLine.HeightRequest = 1;
                bottomLine.BackgroundColor = Color.Silver;

                CategaryStacklayout.Children.Add(mainStackLayout);
                CategaryStacklayout.Children.Add(bottomLine);
                CategaryStacklayout.StyleId = listOfCategarys[i].Category;

				var communicationTapGestureRecognizer = new TapGestureRecognizer();
                communicationTapGestureRecognizer.Tapped +=  (sender, e) => {

                    StackLayout selectedLayout = sender as StackLayout;
                    StackLayout selecteLayoutParent = selectedLayout.Parent as StackLayout;
                    var selectedObject = selectedLayout.Children.Count;
                    var countVal = selecteLayoutParent.Children.Count;
					// var obj = visibleOrInvisible.StyleId;
					//StackLayout selectedStackChildern = selectedLayout.Parent.FindByName<StackLayout>("isVisibleOrInVisibleRef");

					//listOfObjectsPerCategary = await App.Database.GetJsseBehaviors(0, 1);
                    for (int j = 0; j < selecteLayoutParent.Children.Count; j++){
                        Label selectedLabel = selectedLayout.Children[0] as Label;
                        Image selectedImage = selectedLayout.Children[1] as Image;
                        if (selectedLabel.Text == selecteLayoutParent.Children[j].StyleId){
                            if (selecteLayoutParent.Children[j].IsVisible){
                                selecteLayoutParent.Children[j].IsVisible = false;
                                selectedImage.Source = "ArrowDown";
                            }else{
                                selecteLayoutParent.Children[j].IsVisible = true;
                                selectedImage.Source = "Arrowup";
                            }
                        }
                    }
                    JSSEMasterCategory sec = selectedLayout.BindingContext as JSSEMasterCategory;

				};
				mainStackLayout.GestureRecognizers.Add(communicationTapGestureRecognizer);
				communicationTapGestureRecognizer.NumberOfTapsRequired = 1;

                listOfEnterprisePerCategary = await App.Database.GetJsseBehaviors(0, listOfCategarys[i].Category_ID);
                listOfBehaviersPerCategary = await App.Database.GetJsseBehaviors(720, listOfCategarys[i].Category_ID);

                VisibleOrInVisibleScreenDesign(CategaryStacklayout, listOfCategarys[i].Category, listOfCategarys[i].Category_ID);
            }
        }

        public void VisibleOrInVisibleScreenDesign(StackLayout categaryStacklayoutRef, string styleIdRef, int mainCategaryId) {

            StackLayout visibleOrInVisibleMainStack = new StackLayout();
            visibleOrInVisibleMainStack.HorizontalOptions = LayoutOptions.FillAndExpand;
            visibleOrInVisibleMainStack.VerticalOptions = LayoutOptions.Start;
            visibleOrInVisibleMainStack.Orientation = StackOrientation.Vertical;
            visibleOrInVisibleMainStack.Spacing = 0;
            visibleOrInVisibleMainStack.IsVisible = false;
            visibleOrInVisibleMainStack.StyleId = styleIdRef;
            //visibleOrInVisibleMainStack.BackgroundColor = Color.Red;

            StackLayout overAllRatingStack = new StackLayout();
            overAllRatingStack.HorizontalOptions = LayoutOptions.FillAndExpand;
            overAllRatingStack.VerticalOptions = LayoutOptions.Start;


            StackLayout titleAndEditBtnStack = new StackLayout();
            titleAndEditBtnStack.Padding = new Thickness(20, 0, 20, 0);
            titleAndEditBtnStack.Orientation = StackOrientation.Horizontal;
            titleAndEditBtnStack.HorizontalOptions = LayoutOptions.FillAndExpand;
            titleAndEditBtnStack.VerticalOptions = LayoutOptions.Start;
            titleAndEditBtnStack.Spacing = 5;

            Label label = new Label();
            label.HorizontalOptions = LayoutOptions.FillAndExpand;
            label.Text = "Overall rating";
            label.FontSize = 14;

            Button editButton = new Button();
            editButton.Image = "edit";
            editButton.HorizontalOptions = LayoutOptions.End;
            editButton.WidthRequest = 20;
            editButton.HeightRequest = 20;
            editButton.Clicked += (sender, e) => {
                
                Button selectedLayout = sender as Button;
                StackLayout selecteLayoutParent = selectedLayout.Parent.Parent as StackLayout;
                var selectedObject = selecteLayoutParent.Children.Count;
                var countVal = selecteLayoutParent.Children.Count;
                // var obj = visibleOrInvisible.StyleId;
                //StackLayout selectedStackChildern = selectedLayout.Parent.FindByName<StackLayout>("isVisibleOrInVisibleRef");

                //listOfObjectsPerCategary = await App.Database.GetJsseBehaviors(0, 1);
                if (selecteLayoutParent.Children.Count > 1)
                {
                    StackLayout childStackLayout = selecteLayoutParent.Children[1] as StackLayout;
                    if (childStackLayout.Children.Count == 4){

                        StackLayout editorStack = childStackLayout.Children[3] as StackLayout;
                        for (int j = 0; j < editorStack.Children.Count; j++)
                        {
                             SelectedEditor = editorStack.Children[j] as Editor;
                            editorStack.IsVisible = true;
                            CommentsView.IsVisible = true;
                            titleRefLabel.Text = "Overall rating";
                            CommentsEditorref.Text = SelectedEditor.Text;
                        }
                    }

                }



            };


            titleAndEditBtnStack.Children.Add(label);
            titleAndEditBtnStack.Children.Add(editButton);
            overAllRatingStack.Children.Add(titleAndEditBtnStack);

            StackLayout ratingsLayout = new StackLayout();
            ratingsLayout.Orientation = StackOrientation.Vertical;
            ratingsLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
            ratingsLayout.VerticalOptions = LayoutOptions.Start;
            ratingsLayout.Spacing = 5;

            OverAllRatingsDesign(listOfRatings, ratingsLayout, mainCategaryId);

            overAllRatingStack.Children.Add(ratingsLayout);

            visibleOrInVisibleMainStack.Children.Add(overAllRatingStack);

            //if (listOfObjectsPerCategary != null )
            //{
                
            secondPartLayoutDesign(listOfEnterprisePerCategary, visibleOrInVisibleMainStack, "Corporate Factors", mainCategaryId);
			
            ThirdPartLayoutDesign(listOfBehaviersPerCategary, visibleOrInVisibleMainStack, "Organization Behaviors", mainCategaryId);
                categaryStacklayoutRef.Children.Add(visibleOrInVisibleMainStack);
            //}
        }

        public void secondPartLayoutDesign(List<JSSEMasterBehavior> listOfObjectsPerCategary, StackLayout secondStackRef, string titleLabel, int CategaryId){
           

            StackLayout visibleOrInvisibleStack = new StackLayout();
            visibleOrInvisibleStack.HorizontalOptions = LayoutOptions.FillAndExpand;
            visibleOrInvisibleStack.VerticalOptions = LayoutOptions.Start;
            //visibleOrInvisibleStack.BackgroundColor = Color.Red;
            visibleOrInvisibleStack.Orientation = StackOrientation.Vertical;
            visibleOrInvisibleStack.Spacing = 0;
            visibleOrInvisibleStack.Padding =new Thickness(10);
            visibleOrInvisibleStack.StyleId = "subbbbbbbb";

            Frame frame = new Frame();
			frame.Padding = new Thickness(0,10,0,0);
			frame.OutlineColor = Color.Black;
			frame.CornerRadius = 0;
            frame.HasShadow = false;
            frame.StyleId = "hiiiiii";
            //frame.IsVisible = false;
           // frame.Margin = new Thickness(10,0,10,0);

			StackLayout mainStack = new StackLayout();
			mainStack.Orientation = StackOrientation.Vertical;
			mainStack.HorizontalOptions = LayoutOptions.FillAndExpand;
			mainStack.VerticalOptions = LayoutOptions.Start;
			mainStack.Spacing = 0;


           // StackLayout groupTitleStack = null;

            for (int i = 0; i < listOfObjectsPerCategary.Count; i++)
            {

                StackLayout groupTitleStack = new StackLayout();
                groupTitleStack.HorizontalOptions = LayoutOptions.FillAndExpand;
                groupTitleStack.VerticalOptions = LayoutOptions.Start;
                groupTitleStack.Padding = new Thickness(0);
                groupTitleStack.Spacing = 0;
                groupTitleStack.Orientation = StackOrientation.Horizontal;
                groupTitleStack.BackgroundColor = Color.Gray;
             //   groupTitleStack.BackgroundColor = Color.Green;

                StackLayout secondPartStack = new StackLayout();
                secondPartStack.HorizontalOptions = LayoutOptions.FillAndExpand;
                secondPartStack.VerticalOptions = LayoutOptions.Start;
               // secondPartStack.BackgroundColor = Color.Red;
                if (i == 0)
                {

                    Image arrowImage = new Image();
                    arrowImage.Source = "ArrowDown";
                    arrowImage.HorizontalOptions = LayoutOptions.End;
                    arrowImage.VerticalOptions = LayoutOptions.Center;
                    arrowImage.HeightRequest = 25;
                    arrowImage.WidthRequest = 25;
                    
                    //adding heading 
                    Button headingLabel = new Button();
                    headingLabel.Margin = new Thickness(20, 0, 0, 0);
                    headingLabel.HorizontalOptions = LayoutOptions.FillAndExpand;
                    headingLabel.VerticalOptions = LayoutOptions.End;
					headingLabel.Text = titleLabel;
					headingLabel.FontSize = 14;
                    headingLabel.FontAttributes = FontAttributes.Bold;
                    headingLabel.HeightRequest = 30;
                    headingLabel.TextColor = Color.White;
                   // headingLabel.BackgroundColor = Color.Red;
                    groupTitleStack.Children.Add(headingLabel);
                    groupTitleStack.Children.Add(arrowImage);

                    headingLabel.Clicked += (sender, e) => {
						//var oooo = visibleOrInvisibleStack.Children.Count;
						Button buttonRef = sender as Button;
                        StackLayout selecteLayoutParent = buttonRef.Parent.Parent as StackLayout;
                        int childCount = selecteLayoutParent.Children.Count;


                        StackLayout arrowImageparentStack = buttonRef.Parent as StackLayout;

                            Frame SelectedFrame = selecteLayoutParent.Children[selecteLayoutParent.Children.Count-1] as Frame;
                        if (SelectedFrame.IsVisible){
                            Image imageRef = arrowImageparentStack.Children[1] as Image;
                            imageRef.Source = "Arrowup";
                            SelectedFrame.IsVisible = false;
                        }else{
                            Image imageRef = arrowImageparentStack.Children[1] as Image;
                            imageRef.Source = "ArrowDown";
                            SelectedFrame.IsVisible = true;
                        }
                           // SelectedFrame.BackgroundColor = Color.Green;
                          
                    };

                    //groupTitleStack.Padding = new Thickness(0, 10, 0, 0);
                }else{
                    //groupTitleStack.Padding = new Thickness(0, 10, 0, 0);
                }

                StackLayout titleAndEditBtnStack = new StackLayout();
                titleAndEditBtnStack.Padding = new Thickness(20, 0, 20, 0);
                titleAndEditBtnStack.Orientation = StackOrientation.Horizontal;
                titleAndEditBtnStack.HorizontalOptions = LayoutOptions.FillAndExpand;
                titleAndEditBtnStack.VerticalOptions = LayoutOptions.Start;
                titleAndEditBtnStack.Spacing = 5;

                Label label = new Label();
                label.HorizontalOptions = LayoutOptions.FillAndExpand;
                label.Text = listOfObjectsPerCategary[i].Behavior;
                label.FontSize = 14;

                Button editButton = new Button();
                editButton.Image = "edit";
                editButton.HorizontalOptions = LayoutOptions.End;
                editButton.WidthRequest = 20;
                editButton.HeightRequest = 20;
                editButton.StyleId = listOfObjectsPerCategary[i].Behavior;
				editButton.Clicked += (sender, e) => {


					Button selectedLayout = sender as Button;
					StackLayout selecteLayoutParent = selectedLayout.Parent.Parent as StackLayout;
					var selectedObject = selecteLayoutParent.Children.Count;
					var countVal = selecteLayoutParent.Children.Count;
					// var obj = visibleOrInvisible.StyleId;
					//StackLayout selectedStackChildern = selectedLayout.Parent.FindByName<StackLayout>("isVisibleOrInVisibleRef");

					//listOfObjectsPerCategary = await App.Database.GetJsseBehaviors(0, 1);
					if (selecteLayoutParent.Children.Count > 1)
					{
						StackLayout childStackLayout = selecteLayoutParent.Children[selecteLayoutParent.Children.Count-1] as StackLayout;
						if (childStackLayout.Children.Count == 4)
						{

							StackLayout editorStack = childStackLayout.Children[3] as StackLayout;
							for (int j = 0; j < editorStack.Children.Count; j++)
							{
								SelectedEditor = editorStack.Children[j] as Editor;
								editorStack.IsVisible = true;
								CommentsView.IsVisible = true;
                                titleRefLabel.Text = selectedLayout.StyleId;
								CommentsEditorref.Text = SelectedEditor.Text;
							}
						}

					}



				};

                //mainStack.Children.Add(groupTitleStack);
                visibleOrInvisibleStack.Children.Add(groupTitleStack);
                //secondPartStack.Children.Add(groupTitleStack);
                //frame.Content = groupTitleStack;
                titleAndEditBtnStack.Children.Add(label);
                titleAndEditBtnStack.Children.Add(editButton);
                secondPartStack.Children.Add(titleAndEditBtnStack);

                StackLayout ratingsLayout = new StackLayout();
                ratingsLayout.Orientation = StackOrientation.Vertical;
                ratingsLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
                ratingsLayout.VerticalOptions = LayoutOptions.Start;
                ratingsLayout.Spacing = 5;

                SubRatingsDesignLayout(listOfRatings, ratingsLayout ,listOfObjectsPerCategary[i].Behavior_ID, CategaryId);

                secondPartStack.Children.Add(ratingsLayout);
               // visibleOrInvisibleStack.Children.Add(secondPartStack);
                mainStack.Children.Add(secondPartStack);
            }
            //if (visibleOrInvisibleStack != null)
              // visibleOrInvisibleStack.Children.Add(groupTitleStack);
            frame.Content = mainStack;
            visibleOrInvisibleStack.Children.Add(frame);
           // secondStackRef.Children.Add(groupTitleStack);
            secondStackRef.Children.Add(visibleOrInvisibleStack);
           // visibleOrInvisibleStack.BackgroundColor = Color.Red;
		}


        public void ThirdPartLayoutDesign(List<JSSEMasterBehavior> listOfObjectsPerCategary, StackLayout secondStackRef, string titleLabel, int CategaryId)
        {


            StackLayout visibleOrInvisibleStack = new StackLayout();
            visibleOrInvisibleStack.HorizontalOptions = LayoutOptions.FillAndExpand;
            visibleOrInvisibleStack.VerticalOptions = LayoutOptions.Start;
            //visibleOrInvisibleStack.BackgroundColor = Color.Red;
            visibleOrInvisibleStack.Orientation = StackOrientation.Vertical;
            visibleOrInvisibleStack.Spacing = 0;
            visibleOrInvisibleStack.Padding = new Thickness(10);
            visibleOrInvisibleStack.StyleId = "subbbbbbbb";

            Frame frame = new Frame();
            frame.Padding = new Thickness(0,10,0,0);
            frame.OutlineColor = Color.Black;
            frame.CornerRadius = 0;
            frame.HasShadow = false;
            frame.StyleId = "hiiiiii";
            //frame.IsVisible = false;
            // frame.Margin = new Thickness(10,0,10,0);

            StackLayout mainStack = new StackLayout();
            mainStack.Orientation = StackOrientation.Vertical;
            mainStack.HorizontalOptions = LayoutOptions.FillAndExpand;
            mainStack.VerticalOptions = LayoutOptions.Start;
            mainStack.Spacing = 0;


            // StackLayout groupTitleStack = null;

            for (int i = 0; i < listOfObjectsPerCategary.Count; i++)
            {

                StackLayout groupTitleStack = new StackLayout();
                groupTitleStack.HorizontalOptions = LayoutOptions.FillAndExpand;
                groupTitleStack.VerticalOptions = LayoutOptions.Start;
                groupTitleStack.Padding = new Thickness(0);
                groupTitleStack.Spacing = 0;
                groupTitleStack.BackgroundColor = Color.Gray;
                groupTitleStack.Orientation = StackOrientation.Horizontal;

                StackLayout secondPartStack = new StackLayout();
                secondPartStack.HorizontalOptions = LayoutOptions.FillAndExpand;
                secondPartStack.VerticalOptions = LayoutOptions.Start;
                // secondPartStack.BackgroundColor = Color.Red;
                if (i == 0)
                {

                    Image arrowImage = new Image();
                    arrowImage.Source = "ArrowDown";
                    arrowImage.HorizontalOptions = LayoutOptions.End;
                    arrowImage.VerticalOptions = LayoutOptions.Center;
                    arrowImage.HeightRequest = 25;
                    arrowImage.WidthRequest = 25;

                    //adding heading 
                    Button headingLabel = new Button();
                    headingLabel.Margin = new Thickness(20, 0, 0, 0);
                    headingLabel.HorizontalOptions = LayoutOptions.FillAndExpand;
                    headingLabel.VerticalOptions = LayoutOptions.End;
                    headingLabel.Text = titleLabel;
                    headingLabel.TextColor = Color.White;
                    headingLabel.FontSize = 14;
                    headingLabel.FontAttributes = FontAttributes.Bold;
                    headingLabel.HeightRequest = 30;
                    groupTitleStack.Children.Add(headingLabel);
                    groupTitleStack.Children.Add(arrowImage);

                    headingLabel.Clicked += (sender, e) => {
                        //var oooo = visibleOrInvisibleStack.Children.Count;
                        Button buttonRef = sender as Button;
                        StackLayout selecteLayoutParent = buttonRef.Parent.Parent as StackLayout;
                        int childCount = selecteLayoutParent.Children.Count;

                        StackLayout arrowImageparentStack = buttonRef.Parent as StackLayout;

                        Frame SelectedFrame = selecteLayoutParent.Children[selecteLayoutParent.Children.Count - 1] as Frame;
                        if (SelectedFrame.IsVisible)
                        {
                            Image imageRef = arrowImageparentStack.Children[1] as Image;
                            imageRef.Source = "Arrowup";
                            SelectedFrame.IsVisible = false;
                        }
                        else
                        {
                            Image imageRef = arrowImageparentStack.Children[1] as Image;
                            imageRef.Source = "ArrowDown";
                            SelectedFrame.IsVisible = true;
                        }
                        // SelectedFrame.BackgroundColor = Color.Green;

                    };

                    //groupTitleStack.Padding = new Thickness(0, 10, 0, 0);
                }
                else
                {
                    //groupTitleStack.Padding = new Thickness(0, 10, 0, 0);
                }

                StackLayout titleAndEditBtnStack = new StackLayout();
                titleAndEditBtnStack.Padding = new Thickness(20, 0, 20, 0);
                titleAndEditBtnStack.Orientation = StackOrientation.Horizontal;
                titleAndEditBtnStack.HorizontalOptions = LayoutOptions.FillAndExpand;
                titleAndEditBtnStack.VerticalOptions = LayoutOptions.Start;
                titleAndEditBtnStack.Spacing = 5;

                Label label = new Label();
                label.HorizontalOptions = LayoutOptions.FillAndExpand;
                label.Text = listOfObjectsPerCategary[i].Behavior;
                label.FontSize = 14;

                Button editButton = new Button();
                editButton.Image = "edit";
                editButton.HorizontalOptions = LayoutOptions.End;
                editButton.WidthRequest = 20;
                editButton.HeightRequest = 20;
                editButton.StyleId = listOfObjectsPerCategary[i].Behavior;

                editButton.Clicked += (sender, e) => {


                    Button selectedLayout = sender as Button;
                    StackLayout selecteLayoutParent = selectedLayout.Parent.Parent as StackLayout;
                    var selectedObject = selecteLayoutParent.Children.Count;
                    var countVal = selecteLayoutParent.Children.Count;
                    // var obj = visibleOrInvisible.StyleId;
                    //StackLayout selectedStackChildern = selectedLayout.Parent.FindByName<StackLayout>("isVisibleOrInVisibleRef");

                    //listOfObjectsPerCategary = await App.Database.GetJsseBehaviors(0, 1);
                    if (selecteLayoutParent.Children.Count > 1)
                    {
                        StackLayout childStackLayout = selecteLayoutParent.Children[selecteLayoutParent.Children.Count - 1] as StackLayout;
                        if (childStackLayout.Children.Count == 4)
                        {

                            StackLayout editorStack = childStackLayout.Children[3] as StackLayout;
                            for (int j = 0; j < editorStack.Children.Count; j++)
                            {
                                SelectedEditor = editorStack.Children[j] as Editor;
                                editorStack.IsVisible = true;
                                CommentsView.IsVisible = true;
                                titleRefLabel.Text = selectedLayout.StyleId;
                                CommentsEditorref.Text = SelectedEditor.Text;
                            }
                        }

                    }



                };

                //mainStack.Children.Add(groupTitleStack);
                visibleOrInvisibleStack.Children.Add(groupTitleStack);
                //secondPartStack.Children.Add(groupTitleStack);
                //frame.Content = groupTitleStack;
                titleAndEditBtnStack.Children.Add(label);
                titleAndEditBtnStack.Children.Add(editButton);
                secondPartStack.Children.Add(titleAndEditBtnStack);

                StackLayout ratingsLayout = new StackLayout();
                ratingsLayout.Orientation = StackOrientation.Vertical;
                ratingsLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
                ratingsLayout.VerticalOptions = LayoutOptions.Start;
                ratingsLayout.Spacing = 5;

                SubRatingsDesignLayout(listOfRatings, ratingsLayout, listOfObjectsPerCategary[i].Behavior_ID, CategaryId);

                secondPartStack.Children.Add(ratingsLayout);
                // visibleOrInvisibleStack.Children.Add(secondPartStack);
                mainStack.Children.Add(secondPartStack);
            }
            //if (visibleOrInvisibleStack != null)
            // visibleOrInvisibleStack.Children.Add(groupTitleStack);
            frame.Content = mainStack;
            visibleOrInvisibleStack.Children.Add(frame);
            // secondStackRef.Children.Add(groupTitleStack);
            secondStackRef.Children.Add(visibleOrInvisibleStack);
        }


        public void OverAllRatingsDesign(List<RatingTable> listOfRatings, StackLayout layout, int mainId)
		{

			BoxView topLine = new BoxView();
			topLine.HorizontalOptions = LayoutOptions.FillAndExpand;
			topLine.VerticalOptions = LayoutOptions.Start;
			topLine.HeightRequest = 1;
			topLine.BackgroundColor = Color.Silver;
            layout.Children.Add(topLine);


			StackLayout ratingsLayoutSub = new StackLayout();
            ratingsLayoutSub.Orientation = StackOrientation.Horizontal;
			ratingsLayoutSub.HorizontalOptions = LayoutOptions.FillAndExpand;
			ratingsLayoutSub.VerticalOptions = LayoutOptions.Start;
			ratingsLayoutSub.Spacing = 0;
            ratingsLayoutSub.Padding = new Thickness(0);
			

            for (int i = 0; i < listOfRatings.Count; i++)
			{
				Button exceptionalBtn = new Button();
                exceptionalBtn.Text = listOfRatings[i].Rating;
				exceptionalBtn.HorizontalOptions = LayoutOptions.FillAndExpand;
				exceptionalBtn.BackgroundColor = Color.Transparent;
                exceptionalBtn.StyleId = listOfRatings[i].Rating_ID.ToString();
				ratingsLayoutSub.Children.Add(exceptionalBtn);

                exceptionalBtn.Clicked += (sender, e) => {

                    Categories categoriesObj = new Categories();
                    Button selectedButton = sender as Button;

                    int cou = ratingsLayoutSub.Children.Count;
                    StackLayout EditorStackParent = selectedButton.Parent.Parent as StackLayout;

                    if (EditorStackParent.Children.Count == 4){
                        StackLayout editorRefStack = EditorStackParent.Children[3] as StackLayout;
                        Editor editorRef = editorRefStack.Children[0] as Editor;
                        categoriesObj.Comments = editorRef.Text;
                    }

                    for (int m = 0; m < ratingsLayoutSub.Children.Count; m++){
						Button buttenRef = ratingsLayoutSub.Children[m] as Button;
                        if(buttenRef != null){
                            buttenRef.BackgroundColor = Color.Transparent;
                        }
					}
                    selectedButton.BackgroundColor = Color.Red;
                    string selectedratingId = selectedButton.StyleId;
                    int selectedcategaryId = mainId;


                    categoriesObj.Category_ID = mainId;
                    categoriesObj.RatingID = selectedratingId;
                   // categoriesObj.Comments = "";

                    InputCategories.Add(categoriesObj);
                    saveBtnRef.IsEnabled = true;

                };
				

                if (i != listOfRatings.Count - 1)
				{
					BoxView verticalLine = new BoxView();
					verticalLine.HorizontalOptions = LayoutOptions.Start;
					verticalLine.VerticalOptions = LayoutOptions.FillAndExpand;
					verticalLine.WidthRequest = 1;
					verticalLine.BackgroundColor = Color.Silver;
					ratingsLayoutSub.Children.Add(verticalLine);
				}
			}

            layout.Children.Add(ratingsLayoutSub);

			BoxView bottomLine = new BoxView();
			bottomLine.HorizontalOptions = LayoutOptions.FillAndExpand;
			bottomLine.VerticalOptions = LayoutOptions.Start;
			bottomLine.HeightRequest = 1;
			bottomLine.BackgroundColor = Color.Silver;
			layout.Children.Add(bottomLine);

            StackLayout editorStack = new StackLayout();
            editorStack.Padding = new Thickness(20, 0, 0, 0);
            editorStack.HorizontalOptions = LayoutOptions.FillAndExpand;
            editorStack.VerticalOptions = LayoutOptions.Start;
            editorStack.IsVisible = false;

            Editor editor = new Editor();
            editor.HorizontalOptions = LayoutOptions.FillAndExpand;
            editor.VerticalOptions = LayoutOptions.Start;
            editor.FontSize = 12;
            editor.HeightRequest = 20;
            editor.IsEnabled = false;
           // editor.Text = "akjasd sfnajsf safbakf sfbkaf fjwf";

            editorStack.Children.Add(editor);
            layout.Children.Add(editorStack);


		}



        public void SubRatingsDesignLayout(List<RatingTable> listOfRatings, StackLayout layout, int BehaviourId, int CategaryId)
        {

            BoxView topLine = new BoxView();
            topLine.HorizontalOptions = LayoutOptions.FillAndExpand;
            topLine.VerticalOptions = LayoutOptions.Start;
            topLine.HeightRequest = 1;
            topLine.BackgroundColor = Color.Silver;
            layout.Children.Add(topLine);


            StackLayout ratingsLayoutSub = new StackLayout();
            ratingsLayoutSub.Orientation = StackOrientation.Horizontal;
            ratingsLayoutSub.HorizontalOptions = LayoutOptions.FillAndExpand;
            ratingsLayoutSub.VerticalOptions = LayoutOptions.Start;
            ratingsLayoutSub.Spacing = 0;
            ratingsLayoutSub.Padding = new Thickness(0);


            for (int i = 0; i < listOfRatings.Count; i++)
            {
                Button exceptionalBtn = new Button();
                exceptionalBtn.Text = listOfRatings[i].Rating;
                exceptionalBtn.HorizontalOptions = LayoutOptions.FillAndExpand;
                exceptionalBtn.BackgroundColor = Color.Transparent;
                exceptionalBtn.StyleId = listOfRatings[i].Rating_ID.ToString();
                ratingsLayoutSub.Children.Add(exceptionalBtn);

                exceptionalBtn.Clicked += (sender, e) => {
                    EntBehavior entBehavior = new EntBehavior();
                    Button selectedButton = sender as Button;

                    int cou = ratingsLayoutSub.Children.Count;

                    StackLayout EditorStackParent = selectedButton.Parent.Parent as StackLayout;

                    if (EditorStackParent.Children.Count == 4)
                    {
                        StackLayout editorRefStack = EditorStackParent.Children[3] as StackLayout;
                        Editor editorRef = editorRefStack.Children[0] as Editor;
                        entBehavior.Comments = editorRef.Text;
                    }

                    for (int m = 0; m < ratingsLayoutSub.Children.Count; m++)
                    {
                        Button buttenRef = ratingsLayoutSub.Children[m] as Button;
                        if (buttenRef != null)
                        {
                            buttenRef.BackgroundColor = Color.Transparent;
                        }
                    }
                    selectedButton.BackgroundColor = Color.Red;
                    string selectedratingId = selectedButton.StyleId;
                    int selectedBehaviourId = BehaviourId;
                    int selectedCategaryId = CategaryId;

                   
                    entBehavior.Category_ID = CategaryId;
                    entBehavior.Rating_ID = Convert.ToInt32(selectedratingId);
                    entBehavior.Behavior_ID = selectedBehaviourId;

                    InputOrgBehaviors.Add(entBehavior);
                    saveBtnRef.IsEnabled = true;
                      

                };


                if (i != listOfRatings.Count - 1)
                {
                    BoxView verticalLine = new BoxView();
                    verticalLine.HorizontalOptions = LayoutOptions.Start;
                    verticalLine.VerticalOptions = LayoutOptions.FillAndExpand;
                    verticalLine.WidthRequest = 1;
                    verticalLine.BackgroundColor = Color.Silver;
                    ratingsLayoutSub.Children.Add(verticalLine);
                }
            }

            layout.Children.Add(ratingsLayoutSub);

            BoxView bottomLine = new BoxView();
            bottomLine.HorizontalOptions = LayoutOptions.FillAndExpand;
            bottomLine.VerticalOptions = LayoutOptions.Start;
            bottomLine.HeightRequest = 1;
            bottomLine.BackgroundColor = Color.Silver;
            layout.Children.Add(bottomLine);

            StackLayout editorStack = new StackLayout();
            editorStack.Padding = new Thickness(20, 0, 0, 0);
            editorStack.HorizontalOptions = LayoutOptions.FillAndExpand;
            editorStack.VerticalOptions = LayoutOptions.Start;
            editorStack.IsVisible = false;

            Editor editor = new Editor();
            editor.HorizontalOptions = LayoutOptions.FillAndExpand;
            editor.VerticalOptions = LayoutOptions.Start;
            editor.FontSize = 12;
            editor.HeightRequest = 20;
            editor.IsEnabled = false;
            // editor.Text = "akjasd sfnajsf safbakf sfbkaf fjwf";

            editorStack.Children.Add(editor);
            layout.Children.Add(editorStack);


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

		void SaveBtnRef_Clicked(object sender, EventArgs e)
		{
            Debug.WriteLine(InputCategories.Count);
            Debug.WriteLine(InputOrgBehaviors.Count);
		}

    }
}
