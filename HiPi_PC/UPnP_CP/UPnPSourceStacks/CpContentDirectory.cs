using System;
using System.Collections;
using System.Threading;
using OpenSource.Utilities;
using OpenSource.UPnP;

namespace SourceStack
{
    /// <summary>
    /// Transparent ClientSide UPnP Service
    /// </summary>
    public class CpContentDirectory
    {
       private Hashtable UnspecifiedTable = Hashtable.Synchronized(new Hashtable());
       internal UPnPService _S;

       public UPnPService GetUPnPService()
       {
            return(_S);
       }
       public static string SERVICE_NAME = "urn:schemas-upnp-org:service:ContentDirectory:";
       public double VERSION
       {
           get
           {
               return(double.Parse(_S.Version));
           }
       }

       public delegate void StateVariableModifiedHandler_TransferIDs(CpContentDirectory sender, System.String NewValue);
       private WeakEvent OnStateVariable_TransferIDs_Event = new WeakEvent();
       public event StateVariableModifiedHandler_TransferIDs OnStateVariable_TransferIDs
       {
			add{OnStateVariable_TransferIDs_Event.Register(value);}
			remove{OnStateVariable_TransferIDs_Event.UnRegister(value);}
       }
       protected void TransferIDs_ModifiedSink(UPnPStateVariable Var, object NewValue)
       {
            OnStateVariable_TransferIDs_Event.Fire(this, TransferIDs);
       }
       public delegate void StateVariableModifiedHandler_ContainerUpdateIDs(CpContentDirectory sender, System.String NewValue);
       private WeakEvent OnStateVariable_ContainerUpdateIDs_Event = new WeakEvent();
       public event StateVariableModifiedHandler_ContainerUpdateIDs OnStateVariable_ContainerUpdateIDs
       {
			add{OnStateVariable_ContainerUpdateIDs_Event.Register(value);}
			remove{OnStateVariable_ContainerUpdateIDs_Event.UnRegister(value);}
       }
       protected void ContainerUpdateIDs_ModifiedSink(UPnPStateVariable Var, object NewValue)
       {
            OnStateVariable_ContainerUpdateIDs_Event.Fire(this, ContainerUpdateIDs);
       }
       public delegate void StateVariableModifiedHandler_SystemUpdateID(CpContentDirectory sender, System.UInt32 NewValue);
       private WeakEvent OnStateVariable_SystemUpdateID_Event = new WeakEvent();
       public event StateVariableModifiedHandler_SystemUpdateID OnStateVariable_SystemUpdateID
       {
			add{OnStateVariable_SystemUpdateID_Event.Register(value);}
			remove{OnStateVariable_SystemUpdateID_Event.UnRegister(value);}
       }
       protected void SystemUpdateID_ModifiedSink(UPnPStateVariable Var, object NewValue)
       {
            OnStateVariable_SystemUpdateID_Event.Fire(this, SystemUpdateID);
       }
       public delegate void SubscribeHandler(CpContentDirectory sender, bool Success);
       public event SubscribeHandler OnSubscribe;
       public delegate void Delegate_OnResult_Browse(CpContentDirectory sender, System.String ObjectID, Enum_A_ARG_TYPE_BrowseFlag BrowseFlag, System.String Filter, System.UInt32 StartingIndex, System.UInt32 RequestedCount, System.String SortCriteria, System.String Result, System.UInt32 NumberReturned, System.UInt32 TotalMatches, System.UInt32 UpdateID, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_Browse_Event = new WeakEvent();
       public event Delegate_OnResult_Browse OnResult_Browse
       {
			add{OnResult_Browse_Event.Register(value);}
			remove{OnResult_Browse_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_GetSearchCapabilities(CpContentDirectory sender, System.String SearchCaps, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_GetSearchCapabilities_Event = new WeakEvent();
       public event Delegate_OnResult_GetSearchCapabilities OnResult_GetSearchCapabilities
       {
			add{OnResult_GetSearchCapabilities_Event.Register(value);}
			remove{OnResult_GetSearchCapabilities_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_GetSortCapabilities(CpContentDirectory sender, System.String SortCaps, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_GetSortCapabilities_Event = new WeakEvent();
       public event Delegate_OnResult_GetSortCapabilities OnResult_GetSortCapabilities
       {
			add{OnResult_GetSortCapabilities_Event.Register(value);}
			remove{OnResult_GetSortCapabilities_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_GetSystemUpdateID(CpContentDirectory sender, System.UInt32 Id, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_GetSystemUpdateID_Event = new WeakEvent();
       public event Delegate_OnResult_GetSystemUpdateID OnResult_GetSystemUpdateID
       {
			add{OnResult_GetSystemUpdateID_Event.Register(value);}
			remove{OnResult_GetSystemUpdateID_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_Search(CpContentDirectory sender, System.String ContainerID, System.String SearchCriteria, System.String Filter, System.UInt32 StartingIndex, System.UInt32 RequestedCount, System.String SortCriteria, System.String Result, System.UInt32 NumberReturned, System.UInt32 TotalMatches, System.UInt32 UpdateID, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_Search_Event = new WeakEvent();
       public event Delegate_OnResult_Search OnResult_Search
       {
			add{OnResult_Search_Event.Register(value);}
			remove{OnResult_Search_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_X_BrowseByLetter(CpContentDirectory sender, System.String ObjectID, Enum_A_ARG_TYPE_BrowseFlag BrowseFlag, System.String Filter, System.String StartingLetter, System.UInt32 RequestedCount, System.String SortCriteria, System.String Result, System.UInt32 NumberReturned, System.UInt32 TotalMatches, System.UInt32 UpdateID, System.UInt32 StartingIndex, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_X_BrowseByLetter_Event = new WeakEvent();
       public event Delegate_OnResult_X_BrowseByLetter OnResult_X_BrowseByLetter
       {
			add{OnResult_X_BrowseByLetter_Event.Register(value);}
			remove{OnResult_X_BrowseByLetter_Event.UnRegister(value);}
       }

        public CpContentDirectory(UPnPService s)
        {
            _S = s;
            _S.OnSubscribe += new UPnPService.UPnPEventSubscribeHandler(_subscribe_sink);
            if (HasStateVariable_TransferIDs) _S.GetStateVariableObject("TransferIDs").OnModified += new UPnPStateVariable.ModifiedHandler(TransferIDs_ModifiedSink);
            if (HasStateVariable_ContainerUpdateIDs) _S.GetStateVariableObject("ContainerUpdateIDs").OnModified += new UPnPStateVariable.ModifiedHandler(ContainerUpdateIDs_ModifiedSink);
            if (HasStateVariable_SystemUpdateID) _S.GetStateVariableObject("SystemUpdateID").OnModified += new UPnPStateVariable.ModifiedHandler(SystemUpdateID_ModifiedSink);
        }
        public void Dispose()
        {
            _S.OnSubscribe -= new UPnPService.UPnPEventSubscribeHandler(_subscribe_sink);
            OnSubscribe = null;
            if (HasStateVariable_TransferIDs) _S.GetStateVariableObject("TransferIDs").OnModified -= new UPnPStateVariable.ModifiedHandler(TransferIDs_ModifiedSink);
            if (HasStateVariable_ContainerUpdateIDs) _S.GetStateVariableObject("ContainerUpdateIDs").OnModified -= new UPnPStateVariable.ModifiedHandler(ContainerUpdateIDs_ModifiedSink);
            if (HasStateVariable_SystemUpdateID) _S.GetStateVariableObject("SystemUpdateID").OnModified -= new UPnPStateVariable.ModifiedHandler(SystemUpdateID_ModifiedSink);
        }
        public void _subscribe(int Timeout)
        {
            _S.Subscribe(Timeout, null);
        }
        protected void _subscribe_sink(UPnPService sender, bool OK)
        {
            if (OnSubscribe!=null)
            {
                OnSubscribe(this, OK);
            }
        }
        public void SetUnspecifiedValue(string EnumType, string val)
        {
            string hash = Thread.CurrentThread.GetHashCode().ToString() + ":" + EnumType;
            UnspecifiedTable[hash] = val;
        }
        public string GetUnspecifiedValue(string EnumType)
        {
            string hash = Thread.CurrentThread.GetHashCode().ToString() + ":" + EnumType;
            if (UnspecifiedTable.ContainsKey(hash)==false)
            {
               return("");
            }
            string RetVal = (string)UnspecifiedTable[hash];
            return(RetVal);
        }
        public string[] Values_A_ARG_TYPE_BrowseFlag
        {
            get
            {
                UPnPStateVariable sv = _S.GetStateVariableObject("A_ARG_TYPE_BrowseFlag");
                return(sv.AllowedStringValues);
            }
        }
        public string Enum_A_ARG_TYPE_BrowseFlag_ToString(Enum_A_ARG_TYPE_BrowseFlag en)
        {
            string RetVal = "";
            switch(en)
            {
                case Enum_A_ARG_TYPE_BrowseFlag.BROWSEMETADATA:
                    RetVal = "BrowseMetadata";
                    break;
                case Enum_A_ARG_TYPE_BrowseFlag.BROWSEDIRECTCHILDREN:
                    RetVal = "BrowseDirectChildren";
                    break;
                case Enum_A_ARG_TYPE_BrowseFlag._UNSPECIFIED_:
                    RetVal = GetUnspecifiedValue("Enum_A_ARG_TYPE_BrowseFlag");
                    break;
            }
            return(RetVal);
        }
        public enum Enum_A_ARG_TYPE_BrowseFlag
        {
            _UNSPECIFIED_,
            BROWSEMETADATA,
            BROWSEDIRECTCHILDREN,
        }
        public Enum_A_ARG_TYPE_BrowseFlag A_ARG_TYPE_BrowseFlag
        {
            get
            {
               Enum_A_ARG_TYPE_BrowseFlag RetVal = 0;
               string v = (string)_S.GetStateVariable("A_ARG_TYPE_BrowseFlag");
               switch(v)
               {
                  case "BrowseMetadata":
                     RetVal = Enum_A_ARG_TYPE_BrowseFlag.BROWSEMETADATA;
                     break;
                  case "BrowseDirectChildren":
                     RetVal = Enum_A_ARG_TYPE_BrowseFlag.BROWSEDIRECTCHILDREN;
                     break;
                  default:
                     RetVal = Enum_A_ARG_TYPE_BrowseFlag._UNSPECIFIED_;
                     SetUnspecifiedValue("Enum_A_ARG_TYPE_BrowseFlag", v);
                     break;
               }
               return(RetVal);
           }
        }
        public System.String SortCapabilities
        {
            get
            {
               return((System.String)_S.GetStateVariable("SortCapabilities"));
            }
        }
        public System.String A_ARG_TYPE_SearchCriteria
        {
            get
            {
               return((System.String)_S.GetStateVariable("A_ARG_TYPE_SearchCriteria"));
            }
        }
        public System.String A_ARG_TYPE_BrowseLetter
        {
            get
            {
               return((System.String)_S.GetStateVariable("A_ARG_TYPE_BrowseLetter"));
            }
        }
        public System.String A_ARG_TYPE_SortCriteria
        {
            get
            {
               return((System.String)_S.GetStateVariable("A_ARG_TYPE_SortCriteria"));
            }
        }
        public System.String A_ARG_TYPE_Result
        {
            get
            {
               return((System.String)_S.GetStateVariable("A_ARG_TYPE_Result"));
            }
        }
        public System.String A_ARG_TYPE_Filter
        {
            get
            {
               return((System.String)_S.GetStateVariable("A_ARG_TYPE_Filter"));
            }
        }
        public System.UInt32 A_ARG_TYPE_UpdateID
        {
            get
            {
               return((System.UInt32)_S.GetStateVariable("A_ARG_TYPE_UpdateID"));
            }
        }
        public System.String TransferIDs
        {
            get
            {
               return((System.String)_S.GetStateVariable("TransferIDs"));
            }
        }
        public System.String SearchCapabilities
        {
            get
            {
               return((System.String)_S.GetStateVariable("SearchCapabilities"));
            }
        }
        public System.String ContainerUpdateIDs
        {
            get
            {
               return((System.String)_S.GetStateVariable("ContainerUpdateIDs"));
            }
        }
        public System.String A_ARG_TYPE_ObjectID
        {
            get
            {
               return((System.String)_S.GetStateVariable("A_ARG_TYPE_ObjectID"));
            }
        }
        public System.UInt32 SystemUpdateID
        {
            get
            {
               return((System.UInt32)_S.GetStateVariable("SystemUpdateID"));
            }
        }
        public System.UInt32 A_ARG_TYPE_Index
        {
            get
            {
               return((System.UInt32)_S.GetStateVariable("A_ARG_TYPE_Index"));
            }
        }
        public System.UInt32 A_ARG_TYPE_Count
        {
            get
            {
               return((System.UInt32)_S.GetStateVariable("A_ARG_TYPE_Count"));
            }
        }
        public bool HasStateVariable_SortCapabilities
        {
            get
            {
               if (_S.GetStateVariableObject("SortCapabilities")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_A_ARG_TYPE_SearchCriteria
        {
            get
            {
               if (_S.GetStateVariableObject("A_ARG_TYPE_SearchCriteria")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_A_ARG_TYPE_BrowseLetter
        {
            get
            {
               if (_S.GetStateVariableObject("A_ARG_TYPE_BrowseLetter")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_A_ARG_TYPE_SortCriteria
        {
            get
            {
               if (_S.GetStateVariableObject("A_ARG_TYPE_SortCriteria")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_A_ARG_TYPE_Result
        {
            get
            {
               if (_S.GetStateVariableObject("A_ARG_TYPE_Result")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_A_ARG_TYPE_Filter
        {
            get
            {
               if (_S.GetStateVariableObject("A_ARG_TYPE_Filter")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_A_ARG_TYPE_UpdateID
        {
            get
            {
               if (_S.GetStateVariableObject("A_ARG_TYPE_UpdateID")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_A_ARG_TYPE_BrowseFlag
        {
            get
            {
               if (_S.GetStateVariableObject("A_ARG_TYPE_BrowseFlag")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_TransferIDs
        {
            get
            {
               if (_S.GetStateVariableObject("TransferIDs")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_SearchCapabilities
        {
            get
            {
               if (_S.GetStateVariableObject("SearchCapabilities")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_ContainerUpdateIDs
        {
            get
            {
               if (_S.GetStateVariableObject("ContainerUpdateIDs")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_A_ARG_TYPE_ObjectID
        {
            get
            {
               if (_S.GetStateVariableObject("A_ARG_TYPE_ObjectID")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_SystemUpdateID
        {
            get
            {
               if (_S.GetStateVariableObject("SystemUpdateID")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_A_ARG_TYPE_Index
        {
            get
            {
               if (_S.GetStateVariableObject("A_ARG_TYPE_Index")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_A_ARG_TYPE_Count
        {
            get
            {
               if (_S.GetStateVariableObject("A_ARG_TYPE_Count")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_Browse
        {
            get
            {
               if (_S.GetAction("Browse")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_GetSearchCapabilities
        {
            get
            {
               if (_S.GetAction("GetSearchCapabilities")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_GetSortCapabilities
        {
            get
            {
               if (_S.GetAction("GetSortCapabilities")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_GetSystemUpdateID
        {
            get
            {
               if (_S.GetAction("GetSystemUpdateID")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_Search
        {
            get
            {
               if (_S.GetAction("Search")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_X_BrowseByLetter
        {
            get
            {
               if (_S.GetAction("X_BrowseByLetter")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public void Sync_Browse(System.String ObjectID, Enum_A_ARG_TYPE_BrowseFlag BrowseFlag, System.String Filter, System.UInt32 StartingIndex, System.UInt32 RequestedCount, System.String SortCriteria, out System.String Result, out System.UInt32 NumberReturned, out System.UInt32 TotalMatches, out System.UInt32 UpdateID)
        {
           UPnPArgument[] args = new UPnPArgument[10];
           args[0] = new UPnPArgument("ObjectID", ObjectID);
           switch(BrowseFlag)
           {
               case Enum_A_ARG_TYPE_BrowseFlag.BROWSEMETADATA:
                   args[1] = new UPnPArgument("BrowseFlag", "BrowseMetadata");
                   break;
               case Enum_A_ARG_TYPE_BrowseFlag.BROWSEDIRECTCHILDREN:
                   args[1] = new UPnPArgument("BrowseFlag", "BrowseDirectChildren");
                   break;
               default:
                  args[1] = new UPnPArgument("BrowseFlag", GetUnspecifiedValue("Enum_A_ARG_TYPE_BrowseFlag"));
                  break;
           }
           args[2] = new UPnPArgument("Filter", Filter);
           args[3] = new UPnPArgument("StartingIndex", StartingIndex);
           args[4] = new UPnPArgument("RequestedCount", RequestedCount);
           args[5] = new UPnPArgument("SortCriteria", SortCriteria);
           args[6] = new UPnPArgument("Result", "");
           args[7] = new UPnPArgument("NumberReturned", "");
           args[8] = new UPnPArgument("TotalMatches", "");
           args[9] = new UPnPArgument("UpdateID", "");
            _S.InvokeSync("Browse", args);
            for(int i=0;i<args.Length;++i)
            {
                switch(args[i].Name)
                {
                    case "BrowseFlag":
                        switch((string)args[i].DataValue)
                        {
                            case "BrowseMetadata":
                                args[i].DataValue = Enum_A_ARG_TYPE_BrowseFlag.BROWSEMETADATA;
                                break;
                            case "BrowseDirectChildren":
                                args[i].DataValue = Enum_A_ARG_TYPE_BrowseFlag.BROWSEDIRECTCHILDREN;
                                break;
                            default:
                               SetUnspecifiedValue("Enum_A_ARG_TYPE_BrowseFlag", (string)args[i].DataValue);
                               args[i].DataValue = Enum_A_ARG_TYPE_BrowseFlag._UNSPECIFIED_;
                               break;
                        }
                        break;
                }
            }
            ObjectID = (System.String) args[0].DataValue;
            BrowseFlag = (Enum_A_ARG_TYPE_BrowseFlag) args[1].DataValue;
            Filter = (System.String) args[2].DataValue;
            StartingIndex = (System.UInt32) args[3].DataValue;
            RequestedCount = (System.UInt32) args[4].DataValue;
            SortCriteria = (System.String) args[5].DataValue;
            Result = (System.String) args[6].DataValue;
            NumberReturned = (System.UInt32) args[7].DataValue;
            TotalMatches = (System.UInt32) args[8].DataValue;
            UpdateID = (System.UInt32) args[9].DataValue;
            return;
        }
        public void Browse(System.String ObjectID, Enum_A_ARG_TYPE_BrowseFlag BrowseFlag, System.String Filter, System.UInt32 StartingIndex, System.UInt32 RequestedCount, System.String SortCriteria)
        {
            Browse(ObjectID, BrowseFlag, Filter, StartingIndex, RequestedCount, SortCriteria, null, null);
        }
        public void Browse(System.String ObjectID, Enum_A_ARG_TYPE_BrowseFlag BrowseFlag, System.String Filter, System.UInt32 StartingIndex, System.UInt32 RequestedCount, System.String SortCriteria, object _Tag, Delegate_OnResult_Browse _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[10];
           args[0] = new UPnPArgument("ObjectID", ObjectID);
           switch(BrowseFlag)
           {
               case Enum_A_ARG_TYPE_BrowseFlag.BROWSEMETADATA:
                   args[1] = new UPnPArgument("BrowseFlag", "BrowseMetadata");
                   break;
               case Enum_A_ARG_TYPE_BrowseFlag.BROWSEDIRECTCHILDREN:
                   args[1] = new UPnPArgument("BrowseFlag", "BrowseDirectChildren");
                   break;
               default:
                  args[1] = new UPnPArgument("BrowseFlag", GetUnspecifiedValue("Enum_A_ARG_TYPE_BrowseFlag"));
                  break;
           }
           args[2] = new UPnPArgument("Filter", Filter);
           args[3] = new UPnPArgument("StartingIndex", StartingIndex);
           args[4] = new UPnPArgument("RequestedCount", RequestedCount);
           args[5] = new UPnPArgument("SortCriteria", SortCriteria);
           args[6] = new UPnPArgument("Result", "");
           args[7] = new UPnPArgument("NumberReturned", "");
           args[8] = new UPnPArgument("TotalMatches", "");
           args[9] = new UPnPArgument("UpdateID", "");
           _S.InvokeAsync("Browse", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_Browse), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_Browse));
        }
        private void Sink_Browse(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            for(int i=0;i<Args.Length;++i)
            {
                switch(Args[i].Name)
                {
                    case "BrowseFlag":
                        switch((string)Args[i].DataValue)
                        {
                            case "BrowseMetadata":
                                Args[i].DataValue = Enum_A_ARG_TYPE_BrowseFlag.BROWSEMETADATA;
                                break;
                            case "BrowseDirectChildren":
                                Args[i].DataValue = Enum_A_ARG_TYPE_BrowseFlag.BROWSEDIRECTCHILDREN;
                                break;
                            default:
                               SetUnspecifiedValue("Enum_A_ARG_TYPE_BrowseFlag", (string)Args[i].DataValue);
                               Args[i].DataValue = Enum_A_ARG_TYPE_BrowseFlag._UNSPECIFIED_;
                               break;
                        }
                        break;
                }
            }
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_Browse)StateInfo[1])(this, (System.String )Args[0].DataValue, (Enum_A_ARG_TYPE_BrowseFlag )Args[1].DataValue, (System.String )Args[2].DataValue, (System.UInt32 )Args[3].DataValue, (System.UInt32 )Args[4].DataValue, (System.String )Args[5].DataValue, (System.String )Args[6].DataValue, (System.UInt32 )Args[7].DataValue, (System.UInt32 )Args[8].DataValue, (System.UInt32 )Args[9].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_Browse_Event.Fire(this, (System.String )Args[0].DataValue, (Enum_A_ARG_TYPE_BrowseFlag )Args[1].DataValue, (System.String )Args[2].DataValue, (System.UInt32 )Args[3].DataValue, (System.UInt32 )Args[4].DataValue, (System.String )Args[5].DataValue, (System.String )Args[6].DataValue, (System.UInt32 )Args[7].DataValue, (System.UInt32 )Args[8].DataValue, (System.UInt32 )Args[9].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_Browse(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            for(int i=0;i<Args.Length;++i)
            {
                switch(Args[i].Name)
                {
                    case "BrowseFlag":
                        switch((string)Args[i].DataValue)
                        {
                            case "BrowseMetadata":
                                Args[i].DataValue = Enum_A_ARG_TYPE_BrowseFlag.BROWSEMETADATA;
                                break;
                            case "BrowseDirectChildren":
                                Args[i].DataValue = Enum_A_ARG_TYPE_BrowseFlag.BROWSEDIRECTCHILDREN;
                                break;
                        }
                        break;
                }
            }
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_Browse)StateInfo[1])(this, (System.String )Args[0].DataValue, (Enum_A_ARG_TYPE_BrowseFlag )Args[1].DataValue, (System.String )Args[2].DataValue, (System.UInt32 )Args[3].DataValue, (System.UInt32 )Args[4].DataValue, (System.String )Args[5].DataValue, (System.String)UPnPService.CreateObjectInstance(typeof(System.String),null), (System.UInt32)UPnPService.CreateObjectInstance(typeof(System.UInt32),null), (System.UInt32)UPnPService.CreateObjectInstance(typeof(System.UInt32),null), (System.UInt32)UPnPService.CreateObjectInstance(typeof(System.UInt32),null), e, StateInfo[0]);
            }
            else
            {
                OnResult_Browse_Event.Fire(this, (System.String )Args[0].DataValue, (Enum_A_ARG_TYPE_BrowseFlag )Args[1].DataValue, (System.String )Args[2].DataValue, (System.UInt32 )Args[3].DataValue, (System.UInt32 )Args[4].DataValue, (System.String )Args[5].DataValue, (System.String)UPnPService.CreateObjectInstance(typeof(System.String),null), (System.UInt32)UPnPService.CreateObjectInstance(typeof(System.UInt32),null), (System.UInt32)UPnPService.CreateObjectInstance(typeof(System.UInt32),null), (System.UInt32)UPnPService.CreateObjectInstance(typeof(System.UInt32),null), e, StateInfo[0]);
            }
        }
        public void Sync_GetSearchCapabilities(out System.String SearchCaps)
        {
           UPnPArgument[] args = new UPnPArgument[1];
           args[0] = new UPnPArgument("SearchCaps", "");
            _S.InvokeSync("GetSearchCapabilities", args);
            SearchCaps = (System.String) args[0].DataValue;
            return;
        }
        public void GetSearchCapabilities()
        {
            GetSearchCapabilities(null, null);
        }
        public void GetSearchCapabilities(object _Tag, Delegate_OnResult_GetSearchCapabilities _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[1];
           args[0] = new UPnPArgument("SearchCaps", "");
           _S.InvokeAsync("GetSearchCapabilities", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_GetSearchCapabilities), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_GetSearchCapabilities));
        }
        private void Sink_GetSearchCapabilities(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_GetSearchCapabilities)StateInfo[1])(this, (System.String )Args[0].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_GetSearchCapabilities_Event.Fire(this, (System.String )Args[0].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_GetSearchCapabilities(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_GetSearchCapabilities)StateInfo[1])(this, (System.String)UPnPService.CreateObjectInstance(typeof(System.String),null), e, StateInfo[0]);
            }
            else
            {
                OnResult_GetSearchCapabilities_Event.Fire(this, (System.String)UPnPService.CreateObjectInstance(typeof(System.String),null), e, StateInfo[0]);
            }
        }
        public void Sync_GetSortCapabilities(out System.String SortCaps)
        {
           UPnPArgument[] args = new UPnPArgument[1];
           args[0] = new UPnPArgument("SortCaps", "");
            _S.InvokeSync("GetSortCapabilities", args);
            SortCaps = (System.String) args[0].DataValue;
            return;
        }
        public void GetSortCapabilities()
        {
            GetSortCapabilities(null, null);
        }
        public void GetSortCapabilities(object _Tag, Delegate_OnResult_GetSortCapabilities _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[1];
           args[0] = new UPnPArgument("SortCaps", "");
           _S.InvokeAsync("GetSortCapabilities", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_GetSortCapabilities), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_GetSortCapabilities));
        }
        private void Sink_GetSortCapabilities(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_GetSortCapabilities)StateInfo[1])(this, (System.String )Args[0].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_GetSortCapabilities_Event.Fire(this, (System.String )Args[0].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_GetSortCapabilities(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_GetSortCapabilities)StateInfo[1])(this, (System.String)UPnPService.CreateObjectInstance(typeof(System.String),null), e, StateInfo[0]);
            }
            else
            {
                OnResult_GetSortCapabilities_Event.Fire(this, (System.String)UPnPService.CreateObjectInstance(typeof(System.String),null), e, StateInfo[0]);
            }
        }
        public void Sync_GetSystemUpdateID(out System.UInt32 Id)
        {
           UPnPArgument[] args = new UPnPArgument[1];
           args[0] = new UPnPArgument("Id", "");
            _S.InvokeSync("GetSystemUpdateID", args);
            Id = (System.UInt32) args[0].DataValue;
            return;
        }
        public void GetSystemUpdateID()
        {
            GetSystemUpdateID(null, null);
        }
        public void GetSystemUpdateID(object _Tag, Delegate_OnResult_GetSystemUpdateID _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[1];
           args[0] = new UPnPArgument("Id", "");
           _S.InvokeAsync("GetSystemUpdateID", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_GetSystemUpdateID), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_GetSystemUpdateID));
        }
        private void Sink_GetSystemUpdateID(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_GetSystemUpdateID)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_GetSystemUpdateID_Event.Fire(this, (System.UInt32 )Args[0].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_GetSystemUpdateID(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_GetSystemUpdateID)StateInfo[1])(this, (System.UInt32)UPnPService.CreateObjectInstance(typeof(System.UInt32),null), e, StateInfo[0]);
            }
            else
            {
                OnResult_GetSystemUpdateID_Event.Fire(this, (System.UInt32)UPnPService.CreateObjectInstance(typeof(System.UInt32),null), e, StateInfo[0]);
            }
        }
        public void Sync_Search(System.String ContainerID, System.String SearchCriteria, System.String Filter, System.UInt32 StartingIndex, System.UInt32 RequestedCount, System.String SortCriteria, out System.String Result, out System.UInt32 NumberReturned, out System.UInt32 TotalMatches, out System.UInt32 UpdateID)
        {
           UPnPArgument[] args = new UPnPArgument[10];
           args[0] = new UPnPArgument("ContainerID", ContainerID);
           args[1] = new UPnPArgument("SearchCriteria", SearchCriteria);
           args[2] = new UPnPArgument("Filter", Filter);
           args[3] = new UPnPArgument("StartingIndex", StartingIndex);
           args[4] = new UPnPArgument("RequestedCount", RequestedCount);
           args[5] = new UPnPArgument("SortCriteria", SortCriteria);
           args[6] = new UPnPArgument("Result", "");
           args[7] = new UPnPArgument("NumberReturned", "");
           args[8] = new UPnPArgument("TotalMatches", "");
           args[9] = new UPnPArgument("UpdateID", "");
            _S.InvokeSync("Search", args);
            ContainerID = (System.String) args[0].DataValue;
            SearchCriteria = (System.String) args[1].DataValue;
            Filter = (System.String) args[2].DataValue;
            StartingIndex = (System.UInt32) args[3].DataValue;
            RequestedCount = (System.UInt32) args[4].DataValue;
            SortCriteria = (System.String) args[5].DataValue;
            Result = (System.String) args[6].DataValue;
            NumberReturned = (System.UInt32) args[7].DataValue;
            TotalMatches = (System.UInt32) args[8].DataValue;
            UpdateID = (System.UInt32) args[9].DataValue;
            return;
        }
        public void Search(System.String ContainerID, System.String SearchCriteria, System.String Filter, System.UInt32 StartingIndex, System.UInt32 RequestedCount, System.String SortCriteria)
        {
            Search(ContainerID, SearchCriteria, Filter, StartingIndex, RequestedCount, SortCriteria, null, null);
        }
        public void Search(System.String ContainerID, System.String SearchCriteria, System.String Filter, System.UInt32 StartingIndex, System.UInt32 RequestedCount, System.String SortCriteria, object _Tag, Delegate_OnResult_Search _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[10];
           args[0] = new UPnPArgument("ContainerID", ContainerID);
           args[1] = new UPnPArgument("SearchCriteria", SearchCriteria);
           args[2] = new UPnPArgument("Filter", Filter);
           args[3] = new UPnPArgument("StartingIndex", StartingIndex);
           args[4] = new UPnPArgument("RequestedCount", RequestedCount);
           args[5] = new UPnPArgument("SortCriteria", SortCriteria);
           args[6] = new UPnPArgument("Result", "");
           args[7] = new UPnPArgument("NumberReturned", "");
           args[8] = new UPnPArgument("TotalMatches", "");
           args[9] = new UPnPArgument("UpdateID", "");
           _S.InvokeAsync("Search", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_Search), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_Search));
        }
        private void Sink_Search(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_Search)StateInfo[1])(this, (System.String )Args[0].DataValue, (System.String )Args[1].DataValue, (System.String )Args[2].DataValue, (System.UInt32 )Args[3].DataValue, (System.UInt32 )Args[4].DataValue, (System.String )Args[5].DataValue, (System.String )Args[6].DataValue, (System.UInt32 )Args[7].DataValue, (System.UInt32 )Args[8].DataValue, (System.UInt32 )Args[9].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_Search_Event.Fire(this, (System.String )Args[0].DataValue, (System.String )Args[1].DataValue, (System.String )Args[2].DataValue, (System.UInt32 )Args[3].DataValue, (System.UInt32 )Args[4].DataValue, (System.String )Args[5].DataValue, (System.String )Args[6].DataValue, (System.UInt32 )Args[7].DataValue, (System.UInt32 )Args[8].DataValue, (System.UInt32 )Args[9].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_Search(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_Search)StateInfo[1])(this, (System.String )Args[0].DataValue, (System.String )Args[1].DataValue, (System.String )Args[2].DataValue, (System.UInt32 )Args[3].DataValue, (System.UInt32 )Args[4].DataValue, (System.String )Args[5].DataValue, (System.String)UPnPService.CreateObjectInstance(typeof(System.String),null), (System.UInt32)UPnPService.CreateObjectInstance(typeof(System.UInt32),null), (System.UInt32)UPnPService.CreateObjectInstance(typeof(System.UInt32),null), (System.UInt32)UPnPService.CreateObjectInstance(typeof(System.UInt32),null), e, StateInfo[0]);
            }
            else
            {
                OnResult_Search_Event.Fire(this, (System.String )Args[0].DataValue, (System.String )Args[1].DataValue, (System.String )Args[2].DataValue, (System.UInt32 )Args[3].DataValue, (System.UInt32 )Args[4].DataValue, (System.String )Args[5].DataValue, (System.String)UPnPService.CreateObjectInstance(typeof(System.String),null), (System.UInt32)UPnPService.CreateObjectInstance(typeof(System.UInt32),null), (System.UInt32)UPnPService.CreateObjectInstance(typeof(System.UInt32),null), (System.UInt32)UPnPService.CreateObjectInstance(typeof(System.UInt32),null), e, StateInfo[0]);
            }
        }
        public void Sync_X_BrowseByLetter(System.String ObjectID, Enum_A_ARG_TYPE_BrowseFlag BrowseFlag, System.String Filter, System.String StartingLetter, System.UInt32 RequestedCount, System.String SortCriteria, out System.String Result, out System.UInt32 NumberReturned, out System.UInt32 TotalMatches, out System.UInt32 UpdateID, out System.UInt32 StartingIndex)
        {
           UPnPArgument[] args = new UPnPArgument[11];
           args[0] = new UPnPArgument("ObjectID", ObjectID);
           switch(BrowseFlag)
           {
               case Enum_A_ARG_TYPE_BrowseFlag.BROWSEMETADATA:
                   args[1] = new UPnPArgument("BrowseFlag", "BrowseMetadata");
                   break;
               case Enum_A_ARG_TYPE_BrowseFlag.BROWSEDIRECTCHILDREN:
                   args[1] = new UPnPArgument("BrowseFlag", "BrowseDirectChildren");
                   break;
               default:
                  args[1] = new UPnPArgument("BrowseFlag", GetUnspecifiedValue("Enum_A_ARG_TYPE_BrowseFlag"));
                  break;
           }
           args[2] = new UPnPArgument("Filter", Filter);
           args[3] = new UPnPArgument("StartingLetter", StartingLetter);
           args[4] = new UPnPArgument("RequestedCount", RequestedCount);
           args[5] = new UPnPArgument("SortCriteria", SortCriteria);
           args[6] = new UPnPArgument("Result", "");
           args[7] = new UPnPArgument("NumberReturned", "");
           args[8] = new UPnPArgument("TotalMatches", "");
           args[9] = new UPnPArgument("UpdateID", "");
           args[10] = new UPnPArgument("StartingIndex", "");
            _S.InvokeSync("X_BrowseByLetter", args);
            for(int i=0;i<args.Length;++i)
            {
                switch(args[i].Name)
                {
                    case "BrowseFlag":
                        switch((string)args[i].DataValue)
                        {
                            case "BrowseMetadata":
                                args[i].DataValue = Enum_A_ARG_TYPE_BrowseFlag.BROWSEMETADATA;
                                break;
                            case "BrowseDirectChildren":
                                args[i].DataValue = Enum_A_ARG_TYPE_BrowseFlag.BROWSEDIRECTCHILDREN;
                                break;
                            default:
                               SetUnspecifiedValue("Enum_A_ARG_TYPE_BrowseFlag", (string)args[i].DataValue);
                               args[i].DataValue = Enum_A_ARG_TYPE_BrowseFlag._UNSPECIFIED_;
                               break;
                        }
                        break;
                }
            }
            ObjectID = (System.String) args[0].DataValue;
            BrowseFlag = (Enum_A_ARG_TYPE_BrowseFlag) args[1].DataValue;
            Filter = (System.String) args[2].DataValue;
            StartingLetter = (System.String) args[3].DataValue;
            RequestedCount = (System.UInt32) args[4].DataValue;
            SortCriteria = (System.String) args[5].DataValue;
            Result = (System.String) args[6].DataValue;
            NumberReturned = (System.UInt32) args[7].DataValue;
            TotalMatches = (System.UInt32) args[8].DataValue;
            UpdateID = (System.UInt32) args[9].DataValue;
            StartingIndex = (System.UInt32) args[10].DataValue;
            return;
        }
        public void X_BrowseByLetter(System.String ObjectID, Enum_A_ARG_TYPE_BrowseFlag BrowseFlag, System.String Filter, System.String StartingLetter, System.UInt32 RequestedCount, System.String SortCriteria)
        {
            X_BrowseByLetter(ObjectID, BrowseFlag, Filter, StartingLetter, RequestedCount, SortCriteria, null, null);
        }
        public void X_BrowseByLetter(System.String ObjectID, Enum_A_ARG_TYPE_BrowseFlag BrowseFlag, System.String Filter, System.String StartingLetter, System.UInt32 RequestedCount, System.String SortCriteria, object _Tag, Delegate_OnResult_X_BrowseByLetter _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[11];
           args[0] = new UPnPArgument("ObjectID", ObjectID);
           switch(BrowseFlag)
           {
               case Enum_A_ARG_TYPE_BrowseFlag.BROWSEMETADATA:
                   args[1] = new UPnPArgument("BrowseFlag", "BrowseMetadata");
                   break;
               case Enum_A_ARG_TYPE_BrowseFlag.BROWSEDIRECTCHILDREN:
                   args[1] = new UPnPArgument("BrowseFlag", "BrowseDirectChildren");
                   break;
               default:
                  args[1] = new UPnPArgument("BrowseFlag", GetUnspecifiedValue("Enum_A_ARG_TYPE_BrowseFlag"));
                  break;
           }
           args[2] = new UPnPArgument("Filter", Filter);
           args[3] = new UPnPArgument("StartingLetter", StartingLetter);
           args[4] = new UPnPArgument("RequestedCount", RequestedCount);
           args[5] = new UPnPArgument("SortCriteria", SortCriteria);
           args[6] = new UPnPArgument("Result", "");
           args[7] = new UPnPArgument("NumberReturned", "");
           args[8] = new UPnPArgument("TotalMatches", "");
           args[9] = new UPnPArgument("UpdateID", "");
           args[10] = new UPnPArgument("StartingIndex", "");
           _S.InvokeAsync("X_BrowseByLetter", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_X_BrowseByLetter), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_X_BrowseByLetter));
        }
        private void Sink_X_BrowseByLetter(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            for(int i=0;i<Args.Length;++i)
            {
                switch(Args[i].Name)
                {
                    case "BrowseFlag":
                        switch((string)Args[i].DataValue)
                        {
                            case "BrowseMetadata":
                                Args[i].DataValue = Enum_A_ARG_TYPE_BrowseFlag.BROWSEMETADATA;
                                break;
                            case "BrowseDirectChildren":
                                Args[i].DataValue = Enum_A_ARG_TYPE_BrowseFlag.BROWSEDIRECTCHILDREN;
                                break;
                            default:
                               SetUnspecifiedValue("Enum_A_ARG_TYPE_BrowseFlag", (string)Args[i].DataValue);
                               Args[i].DataValue = Enum_A_ARG_TYPE_BrowseFlag._UNSPECIFIED_;
                               break;
                        }
                        break;
                }
            }
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_X_BrowseByLetter)StateInfo[1])(this, (System.String )Args[0].DataValue, (Enum_A_ARG_TYPE_BrowseFlag )Args[1].DataValue, (System.String )Args[2].DataValue, (System.String )Args[3].DataValue, (System.UInt32 )Args[4].DataValue, (System.String )Args[5].DataValue, (System.String )Args[6].DataValue, (System.UInt32 )Args[7].DataValue, (System.UInt32 )Args[8].DataValue, (System.UInt32 )Args[9].DataValue, (System.UInt32 )Args[10].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_X_BrowseByLetter_Event.Fire(this, (System.String )Args[0].DataValue, (Enum_A_ARG_TYPE_BrowseFlag )Args[1].DataValue, (System.String )Args[2].DataValue, (System.String )Args[3].DataValue, (System.UInt32 )Args[4].DataValue, (System.String )Args[5].DataValue, (System.String )Args[6].DataValue, (System.UInt32 )Args[7].DataValue, (System.UInt32 )Args[8].DataValue, (System.UInt32 )Args[9].DataValue, (System.UInt32 )Args[10].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_X_BrowseByLetter(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            for(int i=0;i<Args.Length;++i)
            {
                switch(Args[i].Name)
                {
                    case "BrowseFlag":
                        switch((string)Args[i].DataValue)
                        {
                            case "BrowseMetadata":
                                Args[i].DataValue = Enum_A_ARG_TYPE_BrowseFlag.BROWSEMETADATA;
                                break;
                            case "BrowseDirectChildren":
                                Args[i].DataValue = Enum_A_ARG_TYPE_BrowseFlag.BROWSEDIRECTCHILDREN;
                                break;
                        }
                        break;
                }
            }
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_X_BrowseByLetter)StateInfo[1])(this, (System.String )Args[0].DataValue, (Enum_A_ARG_TYPE_BrowseFlag )Args[1].DataValue, (System.String )Args[2].DataValue, (System.String )Args[3].DataValue, (System.UInt32 )Args[4].DataValue, (System.String )Args[5].DataValue, (System.String)UPnPService.CreateObjectInstance(typeof(System.String),null), (System.UInt32)UPnPService.CreateObjectInstance(typeof(System.UInt32),null), (System.UInt32)UPnPService.CreateObjectInstance(typeof(System.UInt32),null), (System.UInt32)UPnPService.CreateObjectInstance(typeof(System.UInt32),null), (System.UInt32)UPnPService.CreateObjectInstance(typeof(System.UInt32),null), e, StateInfo[0]);
            }
            else
            {
                OnResult_X_BrowseByLetter_Event.Fire(this, (System.String )Args[0].DataValue, (Enum_A_ARG_TYPE_BrowseFlag )Args[1].DataValue, (System.String )Args[2].DataValue, (System.String )Args[3].DataValue, (System.UInt32 )Args[4].DataValue, (System.String )Args[5].DataValue, (System.String)UPnPService.CreateObjectInstance(typeof(System.String),null), (System.UInt32)UPnPService.CreateObjectInstance(typeof(System.UInt32),null), (System.UInt32)UPnPService.CreateObjectInstance(typeof(System.UInt32),null), (System.UInt32)UPnPService.CreateObjectInstance(typeof(System.UInt32),null), (System.UInt32)UPnPService.CreateObjectInstance(typeof(System.UInt32),null), e, StateInfo[0]);
            }
        }
    }
}