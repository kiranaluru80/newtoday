using System;
using System.Collections.Generic;

namespace SampleDB
{
    public class SaveJsseModel
    {
        public class EntBehavior
        {
            public int Behavior_ID { get; set; }
            public string Behavior { get; set; }
            public string BehaviorDesc { get; set; }
            public int BehaviorType_ID { get; set; }
            public string BehaviorType { get; set; }
            public int Org_ID { get; set; }
            public bool BehviorChecked { get; set; }
            public bool IsActive { get; set; }
            public int Category_ID { get; set; }
            public int Rating_ID { get; set; }
            public string Comments { get; set; }
        }

        public class OrgBehavior
        {
            public int Behavior_ID { get; set; }
            public string Behavior { get; set; }
            public string BehaviorDesc { get; set; }
            public int BehaviorType_ID { get; set; }
            public string BehaviorType { get; set; }
            public int Org_ID { get; set; }
            public bool BehviorChecked { get; set; }
            public bool IsActive { get; set; }
            public int Category_ID { get; set; }
            public int Rating_ID { get; set; }
            public string Comments { get; set; }
        }

        public class Ratings
        {
            public int Rating_ID { get; set; }
            public string Rating { get; set; }
            public bool RatingChecked { get; set; }
            public bool Selected { get; set; }
        }

        public class Categories
        {
            public int Category_ID { get; set; }
            public string Category { get; set; }
            public string CategoryDesc { get; set; }
            public List<EntBehavior> EntBehaviors { get; set; }
            public List<OrgBehavior> OrgBehaviors { get; set; }
            //public List<List<>> AllOrgBehaviors { get; set; }
            public List<Ratings> Ratings { get; set; }
            public string Comments { get; set; }
            public string RatingID { get; set; }
            public bool IsActive { get; set; }
            public int JSSE_ID { get; set; }
        }

        public class Observer
        {
            public int CompanyId { get; set; }
            public int Emp_Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserTitle { get; set; }
            public string Email { get; set; }
            public string User_ID { get; set; }
            public string FullName { get; set; }
            public string MajorGroup_Id { get; set; }
            public string Org_Id { get; set; }
            public string Dept_Id { get; set; }
            public string Section_Id { get; set; }
            public int SecurityUserID { get; set; }
        }

        public class Observer2
        {
            public int CompanyId { get; set; }
            public int Emp_Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserTitle { get; set; }
            public string Email { get; set; }
            public string User_ID { get; set; }
            public string FullName { get; set; }
            public string MajorGroup_Id { get; set; }
            public string Org_Id { get; set; }
            public string Dept_Id { get; set; }
            public string Section_Id { get; set; }
            public int SecurityUserID { get; set; }
        }

        public class Observee
        {
            public int CompanyId { get; set; }
            public int Emp_Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserTitle { get; set; }
            public string Email { get; set; }
            public string User_ID { get; set; }
            public string FullName { get; set; }
            public string MajorGroup_Id { get; set; }
            public string Org_Id { get; set; }
            public string Dept_Id { get; set; }
            public string Section_Id { get; set; }
            public int SecurityUserID { get; set; }
        }

        public class Attachment
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string FileName { get; set; }
            public string Base64ImageString { get; set; }
            public bool IsActive { get; set; }
        }

        public class RootObject
        {
            public int JSSE_ID { get; set; }
            public int Observer_ID { get; set; }
            public int Observee_ID { get; set; }
            public int Hierarchy_ID { get; set; }
            public int Region_ID { get; set; }
            public string Region { get; set; }
            public string JSSETitle { get; set; }
            public string JobName { get; set; }
            public string JobDescription { get; set; }
            public DateTime JSSEDate { get; set; }
            public List<Categories> Categories { get; set; }
            public string JSSEStatus { get; set; }
            public bool IsAnonymous { get; set; }
            public int MajorGroup_Id { get; set; }
            public int Org_Id { get; set; }
            public int Dept_Id { get; set; }
            public int Section_Id { get; set; }
            public string JSSEEnteredBy { get; set; }
            public string Location { get; set; }
            public Observer Observer { get; set; }
            public List<Observer2> Observers { get; set; }
            public List<Observee> Observees { get; set; }
            public string SelObservers { get; set; }
            public string SelObservees { get; set; }
            public bool IsExternal { get; set; }
            public DateTime CreatedDate { get; set; }
            public List<Attachment> Attachments { get; set; }
            public bool IsCreator { get; set; }
            public string Image { get; set; }
            public string ImageName { get; set; }
            public string Base64_JSSE_ID { get; set; }
            public bool IsOBSRAnonymous { get; set; }
        }
    }
}
