//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

namespace MEDIA.Model
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(DonateRecord))]
    [KnownType(typeof(DonationProject))]
    [KnownType(typeof(OrderInfo))]
    [KnownType(typeof(UserAnswer))]
    public partial class User: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int UserId
        {
            get { return _userId; }
            set
            {
                if (_userId != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'UserId' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _userId = value;
                    OnPropertyChanged("UserId");
                }
            }
        }
        private int _userId;
    
        [DataMember]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged("FirstName");
                }
            }
        }
        private string _firstName;
    
        [DataMember]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged("LastName");
                }
            }
        }
        private string _lastName;
    
        [DataMember]
        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                if (_emailAddress != value)
                {
                    _emailAddress = value;
                    OnPropertyChanged("EmailAddress");
                }
            }
        }
        private string _emailAddress;
    
        [DataMember]
        public string LoginPwd
        {
            get { return _loginPwd; }
            set
            {
                if (_loginPwd != value)
                {
                    _loginPwd = value;
                    OnPropertyChanged("LoginPwd");
                }
            }
        }
        private string _loginPwd;
    
        [DataMember]
        public Nullable<bool> Gender
        {
            get { return _gender; }
            set
            {
                if (_gender != value)
                {
                    _gender = value;
                    OnPropertyChanged("Gender");
                }
            }
        }
        private Nullable<bool> _gender;
    
        [DataMember]
        public string Address
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged("Address");
                }
            }
        }
        private string _address;
    
        [DataMember]
        public string Zip
        {
            get { return _zip; }
            set
            {
                if (_zip != value)
                {
                    _zip = value;
                    OnPropertyChanged("Zip");
                }
            }
        }
        private string _zip;
    
        [DataMember]
        public string Town
        {
            get { return _town; }
            set
            {
                if (_town != value)
                {
                    _town = value;
                    OnPropertyChanged("Town");
                }
            }
        }
        private string _town;
    
        [DataMember]
        public string Country
        {
            get { return _country; }
            set
            {
                if (_country != value)
                {
                    _country = value;
                    OnPropertyChanged("Country");
                }
            }
        }
        private string _country;
    
        [DataMember]
        public Nullable<System.DateTime> Birthday
        {
            get { return _birthday; }
            set
            {
                if (_birthday != value)
                {
                    _birthday = value;
                    OnPropertyChanged("Birthday");
                }
            }
        }
        private Nullable<System.DateTime> _birthday;
    
        [DataMember]
        public Nullable<System.DateTime> CreateDate
        {
            get { return _createDate; }
            set
            {
                if (_createDate != value)
                {
                    _createDate = value;
                    OnPropertyChanged("CreateDate");
                }
            }
        }
        private Nullable<System.DateTime> _createDate;
    
        [DataMember]
        public Nullable<bool> IsCheck
        {
            get { return _isCheck; }
            set
            {
                if (_isCheck != value)
                {
                    _isCheck = value;
                    OnPropertyChanged("IsCheck");
                }
            }
        }
        private Nullable<bool> _isCheck;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TrackableCollection<DonateRecord> DonateRecords
        {
            get
            {
                if (_donateRecords == null)
                {
                    _donateRecords = new TrackableCollection<DonateRecord>();
                    _donateRecords.CollectionChanged += FixupDonateRecords;
                }
                return _donateRecords;
            }
            set
            {
                if (!ReferenceEquals(_donateRecords, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_donateRecords != null)
                    {
                        _donateRecords.CollectionChanged -= FixupDonateRecords;
                    }
                    _donateRecords = value;
                    if (_donateRecords != null)
                    {
                        _donateRecords.CollectionChanged += FixupDonateRecords;
                    }
                    OnNavigationPropertyChanged("DonateRecords");
                }
            }
        }
        private TrackableCollection<DonateRecord> _donateRecords;
    
        [DataMember]
        public TrackableCollection<DonationProject> DonationProjects
        {
            get
            {
                if (_donationProjects == null)
                {
                    _donationProjects = new TrackableCollection<DonationProject>();
                    _donationProjects.CollectionChanged += FixupDonationProjects;
                }
                return _donationProjects;
            }
            set
            {
                if (!ReferenceEquals(_donationProjects, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_donationProjects != null)
                    {
                        _donationProjects.CollectionChanged -= FixupDonationProjects;
                    }
                    _donationProjects = value;
                    if (_donationProjects != null)
                    {
                        _donationProjects.CollectionChanged += FixupDonationProjects;
                    }
                    OnNavigationPropertyChanged("DonationProjects");
                }
            }
        }
        private TrackableCollection<DonationProject> _donationProjects;
    
        [DataMember]
        public TrackableCollection<OrderInfo> OrderInfoes
        {
            get
            {
                if (_orderInfoes == null)
                {
                    _orderInfoes = new TrackableCollection<OrderInfo>();
                    _orderInfoes.CollectionChanged += FixupOrderInfoes;
                }
                return _orderInfoes;
            }
            set
            {
                if (!ReferenceEquals(_orderInfoes, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_orderInfoes != null)
                    {
                        _orderInfoes.CollectionChanged -= FixupOrderInfoes;
                    }
                    _orderInfoes = value;
                    if (_orderInfoes != null)
                    {
                        _orderInfoes.CollectionChanged += FixupOrderInfoes;
                    }
                    OnNavigationPropertyChanged("OrderInfoes");
                }
            }
        }
        private TrackableCollection<OrderInfo> _orderInfoes;
    
        [DataMember]
        public TrackableCollection<UserAnswer> UserAnswers
        {
            get
            {
                if (_userAnswers == null)
                {
                    _userAnswers = new TrackableCollection<UserAnswer>();
                    _userAnswers.CollectionChanged += FixupUserAnswers;
                }
                return _userAnswers;
            }
            set
            {
                if (!ReferenceEquals(_userAnswers, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_userAnswers != null)
                    {
                        _userAnswers.CollectionChanged -= FixupUserAnswers;
                    }
                    _userAnswers = value;
                    if (_userAnswers != null)
                    {
                        _userAnswers.CollectionChanged += FixupUserAnswers;
                    }
                    OnNavigationPropertyChanged("UserAnswers");
                }
            }
        }
        private TrackableCollection<UserAnswer> _userAnswers;

        #endregion
        #region ChangeTracking
    
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (ChangeTracker.State != ObjectState.Added && ChangeTracker.State != ObjectState.Deleted)
            {
                ChangeTracker.State = ObjectState.Modified;
            }
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        protected virtual void OnNavigationPropertyChanged(String propertyName)
        {
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged{ add { _propertyChanged += value; } remove { _propertyChanged -= value; } }
        private event PropertyChangedEventHandler _propertyChanged;
        private ObjectChangeTracker _changeTracker;
    
        [DataMember]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if (_changeTracker == null)
                {
                    _changeTracker = new ObjectChangeTracker();
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
                return _changeTracker;
            }
            set
            {
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging -= HandleObjectStateChanging;
                }
                _changeTracker = value;
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
            }
        }
    
        private void HandleObjectStateChanging(object sender, ObjectStateChangingEventArgs e)
        {
            if (e.NewState == ObjectState.Deleted)
            {
                ClearNavigationProperties();
            }
        }
    
        protected bool IsDeserializing { get; private set; }
    
        [OnDeserializing]
        public void OnDeserializingMethod(StreamingContext context)
        {
            IsDeserializing = true;
        }
    
        [OnDeserialized]
        public void OnDeserializedMethod(StreamingContext context)
        {
            IsDeserializing = false;
            ChangeTracker.ChangeTrackingEnabled = true;
        }
    
        protected virtual void ClearNavigationProperties()
        {
            DonateRecords.Clear();
            DonationProjects.Clear();
            OrderInfoes.Clear();
            UserAnswers.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupDonateRecords(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (DonateRecord item in e.NewItems)
                {
                    item.User = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("DonateRecords", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (DonateRecord item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("DonateRecords", item);
                    }
                }
            }
        }
    
        private void FixupDonationProjects(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (DonationProject item in e.NewItems)
                {
                    item.User = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("DonationProjects", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (DonationProject item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("DonationProjects", item);
                    }
                }
            }
        }
    
        private void FixupOrderInfoes(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (OrderInfo item in e.NewItems)
                {
                    item.User = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("OrderInfoes", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (OrderInfo item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("OrderInfoes", item);
                    }
                }
            }
        }
    
        private void FixupUserAnswers(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (UserAnswer item in e.NewItems)
                {
                    item.User = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("UserAnswers", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (UserAnswer item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("UserAnswers", item);
                    }
                }
            }
        }

        #endregion
    }
}
