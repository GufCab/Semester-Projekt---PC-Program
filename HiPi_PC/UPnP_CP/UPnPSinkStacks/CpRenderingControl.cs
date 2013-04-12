using System;
using System.Collections;
using System.Threading;
using OpenSource.Utilities;
using OpenSource.UPnP;

namespace SinkStack
{
    /// <summary>
    /// Transparent ClientSide UPnP Service
    /// </summary>
    public class CpRenderingControl
    {
       private Hashtable UnspecifiedTable = Hashtable.Synchronized(new Hashtable());
       internal UPnPService _S;

       public UPnPService GetUPnPService()
       {
            return(_S);
       }
       public static string SERVICE_NAME = "urn:schemas-upnp-org:service:RenderingControl:";
       public double VERSION
       {
           get
           {
               return(double.Parse(_S.Version));
           }
       }

       public delegate void StateVariableModifiedHandler_LastChange(CpRenderingControl sender, System.String NewValue);
       private WeakEvent OnStateVariable_LastChange_Event = new WeakEvent();
       public event StateVariableModifiedHandler_LastChange OnStateVariable_LastChange
       {
			add{OnStateVariable_LastChange_Event.Register(value);}
			remove{OnStateVariable_LastChange_Event.UnRegister(value);}
       }
       protected void LastChange_ModifiedSink(UPnPStateVariable Var, object NewValue)
       {
            OnStateVariable_LastChange_Event.Fire(this, LastChange);
       }
       public delegate void SubscribeHandler(CpRenderingControl sender, bool Success);
       public event SubscribeHandler OnSubscribe;
       public delegate void Delegate_OnResult_GetMute(CpRenderingControl sender, System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel, System.Boolean CurrentMute, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_GetMute_Event = new WeakEvent();
       public event Delegate_OnResult_GetMute OnResult_GetMute
       {
			add{OnResult_GetMute_Event.Register(value);}
			remove{OnResult_GetMute_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_GetVolume(CpRenderingControl sender, System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel, System.UInt16 CurrentVolume, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_GetVolume_Event = new WeakEvent();
       public event Delegate_OnResult_GetVolume OnResult_GetVolume
       {
			add{OnResult_GetVolume_Event.Register(value);}
			remove{OnResult_GetVolume_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_GetVolumeDB(CpRenderingControl sender, System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel, System.Int16 CurrentVolume, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_GetVolumeDB_Event = new WeakEvent();
       public event Delegate_OnResult_GetVolumeDB OnResult_GetVolumeDB
       {
			add{OnResult_GetVolumeDB_Event.Register(value);}
			remove{OnResult_GetVolumeDB_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_GetVolumeDBRange(CpRenderingControl sender, System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel, System.Int16 MinValue, System.Int16 MaxValue, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_GetVolumeDBRange_Event = new WeakEvent();
       public event Delegate_OnResult_GetVolumeDBRange OnResult_GetVolumeDBRange
       {
			add{OnResult_GetVolumeDBRange_Event.Register(value);}
			remove{OnResult_GetVolumeDBRange_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_ListPresets(CpRenderingControl sender, System.UInt32 InstanceID, Enum_PresetNameList CurrentPresetNameList, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_ListPresets_Event = new WeakEvent();
       public event Delegate_OnResult_ListPresets OnResult_ListPresets
       {
			add{OnResult_ListPresets_Event.Register(value);}
			remove{OnResult_ListPresets_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_SelectPreset(CpRenderingControl sender, System.UInt32 InstanceID, Enum_A_ARG_TYPE_PresetName PresetName, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_SelectPreset_Event = new WeakEvent();
       public event Delegate_OnResult_SelectPreset OnResult_SelectPreset
       {
			add{OnResult_SelectPreset_Event.Register(value);}
			remove{OnResult_SelectPreset_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_SetMute(CpRenderingControl sender, System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel, System.Boolean DesiredMute, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_SetMute_Event = new WeakEvent();
       public event Delegate_OnResult_SetMute OnResult_SetMute
       {
			add{OnResult_SetMute_Event.Register(value);}
			remove{OnResult_SetMute_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_SetVolume(CpRenderingControl sender, System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel, System.UInt16 DesiredVolume, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_SetVolume_Event = new WeakEvent();
       public event Delegate_OnResult_SetVolume OnResult_SetVolume
       {
			add{OnResult_SetVolume_Event.Register(value);}
			remove{OnResult_SetVolume_Event.UnRegister(value);}
       }

        public CpRenderingControl(UPnPService s)
        {
            _S = s;
            _S.OnSubscribe += new UPnPService.UPnPEventSubscribeHandler(_subscribe_sink);
            if (HasStateVariable_LastChange) _S.GetStateVariableObject("LastChange").OnModified += new UPnPStateVariable.ModifiedHandler(LastChange_ModifiedSink);
        }
        public void Dispose()
        {
            _S.OnSubscribe -= new UPnPService.UPnPEventSubscribeHandler(_subscribe_sink);
            OnSubscribe = null;
            if (HasStateVariable_LastChange) _S.GetStateVariableObject("LastChange").OnModified -= new UPnPStateVariable.ModifiedHandler(LastChange_ModifiedSink);
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
        public string[] Values_A_ARG_TYPE_Channel
        {
            get
            {
                UPnPStateVariable sv = _S.GetStateVariableObject("A_ARG_TYPE_Channel");
                return(sv.AllowedStringValues);
            }
        }
        public string Enum_A_ARG_TYPE_Channel_ToString(Enum_A_ARG_TYPE_Channel en)
        {
            string RetVal = "";
            switch(en)
            {
                case Enum_A_ARG_TYPE_Channel.MASTER:
                    RetVal = "Master";
                    break;
                case Enum_A_ARG_TYPE_Channel._UNSPECIFIED_:
                    RetVal = GetUnspecifiedValue("Enum_A_ARG_TYPE_Channel");
                    break;
            }
            return(RetVal);
        }
        public enum Enum_A_ARG_TYPE_Channel
        {
            _UNSPECIFIED_,
            MASTER,
        }
        public Enum_A_ARG_TYPE_Channel A_ARG_TYPE_Channel
        {
            get
            {
               Enum_A_ARG_TYPE_Channel RetVal = 0;
               string v = (string)_S.GetStateVariable("A_ARG_TYPE_Channel");
               switch(v)
               {
                  case "Master":
                     RetVal = Enum_A_ARG_TYPE_Channel.MASTER;
                     break;
                  default:
                     RetVal = Enum_A_ARG_TYPE_Channel._UNSPECIFIED_;
                     SetUnspecifiedValue("Enum_A_ARG_TYPE_Channel", v);
                     break;
               }
               return(RetVal);
           }
        }
        public string[] Values_A_ARG_TYPE_PresetName
        {
            get
            {
                UPnPStateVariable sv = _S.GetStateVariableObject("A_ARG_TYPE_PresetName");
                return(sv.AllowedStringValues);
            }
        }
        public string Enum_A_ARG_TYPE_PresetName_ToString(Enum_A_ARG_TYPE_PresetName en)
        {
            string RetVal = "";
            switch(en)
            {
                case Enum_A_ARG_TYPE_PresetName.FACTORYDEFAULTS:
                    RetVal = "FactoryDefaults";
                    break;
                case Enum_A_ARG_TYPE_PresetName._UNSPECIFIED_:
                    RetVal = GetUnspecifiedValue("Enum_A_ARG_TYPE_PresetName");
                    break;
            }
            return(RetVal);
        }
        public enum Enum_A_ARG_TYPE_PresetName
        {
            _UNSPECIFIED_,
            FACTORYDEFAULTS,
        }
        public Enum_A_ARG_TYPE_PresetName A_ARG_TYPE_PresetName
        {
            get
            {
               Enum_A_ARG_TYPE_PresetName RetVal = 0;
               string v = (string)_S.GetStateVariable("A_ARG_TYPE_PresetName");
               switch(v)
               {
                  case "FactoryDefaults":
                     RetVal = Enum_A_ARG_TYPE_PresetName.FACTORYDEFAULTS;
                     break;
                  default:
                     RetVal = Enum_A_ARG_TYPE_PresetName._UNSPECIFIED_;
                     SetUnspecifiedValue("Enum_A_ARG_TYPE_PresetName", v);
                     break;
               }
               return(RetVal);
           }
        }
        public string[] Values_PresetNameList
        {
            get
            {
                UPnPStateVariable sv = _S.GetStateVariableObject("PresetNameList");
                return(sv.AllowedStringValues);
            }
        }
        public string Enum_PresetNameList_ToString(Enum_PresetNameList en)
        {
            string RetVal = "";
            switch(en)
            {
                case Enum_PresetNameList.FACTORYDEFAULTS:
                    RetVal = "FactoryDefaults";
                    break;
                case Enum_PresetNameList._UNSPECIFIED_:
                    RetVal = GetUnspecifiedValue("Enum_PresetNameList");
                    break;
            }
            return(RetVal);
        }
        public enum Enum_PresetNameList
        {
            _UNSPECIFIED_,
            FACTORYDEFAULTS,
        }
        public Enum_PresetNameList PresetNameList
        {
            get
            {
               Enum_PresetNameList RetVal = 0;
               string v = (string)_S.GetStateVariable("PresetNameList");
               switch(v)
               {
                  case "FactoryDefaults":
                     RetVal = Enum_PresetNameList.FACTORYDEFAULTS;
                     break;
                  default:
                     RetVal = Enum_PresetNameList._UNSPECIFIED_;
                     SetUnspecifiedValue("Enum_PresetNameList", v);
                     break;
               }
               return(RetVal);
           }
        }
        public System.Boolean Mute
        {
            get
            {
               return((System.Boolean)_S.GetStateVariable("Mute"));
            }
        }
        public System.UInt32 A_ARG_TYPE_InstanceID
        {
            get
            {
               return((System.UInt32)_S.GetStateVariable("A_ARG_TYPE_InstanceID"));
            }
        }
        public System.Int16 VolumeDB
        {
            get
            {
               return((System.Int16)_S.GetStateVariable("VolumeDB"));
            }
        }
        public System.UInt16 Volume
        {
            get
            {
               return((System.UInt16)_S.GetStateVariable("Volume"));
            }
        }
        public System.String LastChange
        {
            get
            {
               return((System.String)_S.GetStateVariable("LastChange"));
            }
        }
        public bool HasStateVariable_Mute
        {
            get
            {
               if (_S.GetStateVariableObject("Mute")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_A_ARG_TYPE_InstanceID
        {
            get
            {
               if (_S.GetStateVariableObject("A_ARG_TYPE_InstanceID")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasMaximum_VolumeDB
        {
             get
             {
                 return(_S.GetStateVariableObject("VolumeDB").Maximum!=null);
             }
        }
        public bool HasMinimum_VolumeDB
        {
             get
             {
                 return(_S.GetStateVariableObject("VolumeDB").Minimum!=null);
             }
        }
        public System.Int16 Maximum_VolumeDB
        {
             get
             {
                 return((System.Int16)_S.GetStateVariableObject("VolumeDB").Maximum);
             }
        }
        public System.Int16 Minimum_VolumeDB
        {
             get
             {
                 return((System.Int16)_S.GetStateVariableObject("VolumeDB").Minimum);
             }
        }
        public bool HasStateVariable_VolumeDB
        {
            get
            {
               if (_S.GetStateVariableObject("VolumeDB")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasMaximum_Volume
        {
             get
             {
                 return(_S.GetStateVariableObject("Volume").Maximum!=null);
             }
        }
        public bool HasMinimum_Volume
        {
             get
             {
                 return(_S.GetStateVariableObject("Volume").Minimum!=null);
             }
        }
        public System.UInt16 Maximum_Volume
        {
             get
             {
                 return((System.UInt16)_S.GetStateVariableObject("Volume").Maximum);
             }
        }
        public System.UInt16 Minimum_Volume
        {
             get
             {
                 return((System.UInt16)_S.GetStateVariableObject("Volume").Minimum);
             }
        }
        public bool HasStateVariable_Volume
        {
            get
            {
               if (_S.GetStateVariableObject("Volume")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_A_ARG_TYPE_PresetName
        {
            get
            {
               if (_S.GetStateVariableObject("A_ARG_TYPE_PresetName")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_PresetNameList
        {
            get
            {
               if (_S.GetStateVariableObject("PresetNameList")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_LastChange
        {
            get
            {
               if (_S.GetStateVariableObject("LastChange")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_A_ARG_TYPE_Channel
        {
            get
            {
               if (_S.GetStateVariableObject("A_ARG_TYPE_Channel")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_GetMute
        {
            get
            {
               if (_S.GetAction("GetMute")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_GetVolume
        {
            get
            {
               if (_S.GetAction("GetVolume")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_GetVolumeDB
        {
            get
            {
               if (_S.GetAction("GetVolumeDB")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_GetVolumeDBRange
        {
            get
            {
               if (_S.GetAction("GetVolumeDBRange")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_ListPresets
        {
            get
            {
               if (_S.GetAction("ListPresets")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_SelectPreset
        {
            get
            {
               if (_S.GetAction("SelectPreset")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_SetMute
        {
            get
            {
               if (_S.GetAction("SetMute")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_SetVolume
        {
            get
            {
               if (_S.GetAction("SetVolume")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public void Sync_GetMute(System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel, out System.Boolean CurrentMute)
        {
           UPnPArgument[] args = new UPnPArgument[3];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           switch(Channel)
           {
               case Enum_A_ARG_TYPE_Channel.MASTER:
                   args[1] = new UPnPArgument("Channel", "Master");
                   break;
               default:
                  args[1] = new UPnPArgument("Channel", GetUnspecifiedValue("Enum_A_ARG_TYPE_Channel"));
                  break;
           }
           args[2] = new UPnPArgument("CurrentMute", "");
            _S.InvokeSync("GetMute", args);
            for(int i=0;i<args.Length;++i)
            {
                switch(args[i].Name)
                {
                    case "Channel":
                        switch((string)args[i].DataValue)
                        {
                            case "Master":
                                args[i].DataValue = Enum_A_ARG_TYPE_Channel.MASTER;
                                break;
                            default:
                               SetUnspecifiedValue("Enum_A_ARG_TYPE_Channel", (string)args[i].DataValue);
                               args[i].DataValue = Enum_A_ARG_TYPE_Channel._UNSPECIFIED_;
                               break;
                        }
                        break;
                }
            }
            InstanceID = (System.UInt32) args[0].DataValue;
            Channel = (Enum_A_ARG_TYPE_Channel) args[1].DataValue;
            CurrentMute = (System.Boolean) args[2].DataValue;
            return;
        }
        public void GetMute(System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel)
        {
            GetMute(InstanceID, Channel, null, null);
        }
        public void GetMute(System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel, object _Tag, Delegate_OnResult_GetMute _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[3];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           switch(Channel)
           {
               case Enum_A_ARG_TYPE_Channel.MASTER:
                   args[1] = new UPnPArgument("Channel", "Master");
                   break;
               default:
                  args[1] = new UPnPArgument("Channel", GetUnspecifiedValue("Enum_A_ARG_TYPE_Channel"));
                  break;
           }
           args[2] = new UPnPArgument("CurrentMute", "");
           _S.InvokeAsync("GetMute", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_GetMute), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_GetMute));
        }
        private void Sink_GetMute(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            for(int i=0;i<Args.Length;++i)
            {
                switch(Args[i].Name)
                {
                    case "Channel":
                        switch((string)Args[i].DataValue)
                        {
                            case "Master":
                                Args[i].DataValue = Enum_A_ARG_TYPE_Channel.MASTER;
                                break;
                            default:
                               SetUnspecifiedValue("Enum_A_ARG_TYPE_Channel", (string)Args[i].DataValue);
                               Args[i].DataValue = Enum_A_ARG_TYPE_Channel._UNSPECIFIED_;
                               break;
                        }
                        break;
                }
            }
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_GetMute)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.Boolean )Args[2].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_GetMute_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.Boolean )Args[2].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_GetMute(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            for(int i=0;i<Args.Length;++i)
            {
                switch(Args[i].Name)
                {
                    case "Channel":
                        switch((string)Args[i].DataValue)
                        {
                            case "Master":
                                Args[i].DataValue = Enum_A_ARG_TYPE_Channel.MASTER;
                                break;
                        }
                        break;
                }
            }
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_GetMute)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.Boolean)UPnPService.CreateObjectInstance(typeof(System.Boolean),null), e, StateInfo[0]);
            }
            else
            {
                OnResult_GetMute_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.Boolean)UPnPService.CreateObjectInstance(typeof(System.Boolean),null), e, StateInfo[0]);
            }
        }
        public void Sync_GetVolume(System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel, out System.UInt16 CurrentVolume)
        {
           UPnPArgument[] args = new UPnPArgument[3];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           switch(Channel)
           {
               case Enum_A_ARG_TYPE_Channel.MASTER:
                   args[1] = new UPnPArgument("Channel", "Master");
                   break;
               default:
                  args[1] = new UPnPArgument("Channel", GetUnspecifiedValue("Enum_A_ARG_TYPE_Channel"));
                  break;
           }
           args[2] = new UPnPArgument("CurrentVolume", "");
            _S.InvokeSync("GetVolume", args);
            for(int i=0;i<args.Length;++i)
            {
                switch(args[i].Name)
                {
                    case "Channel":
                        switch((string)args[i].DataValue)
                        {
                            case "Master":
                                args[i].DataValue = Enum_A_ARG_TYPE_Channel.MASTER;
                                break;
                            default:
                               SetUnspecifiedValue("Enum_A_ARG_TYPE_Channel", (string)args[i].DataValue);
                               args[i].DataValue = Enum_A_ARG_TYPE_Channel._UNSPECIFIED_;
                               break;
                        }
                        break;
                }
            }
            InstanceID = (System.UInt32) args[0].DataValue;
            Channel = (Enum_A_ARG_TYPE_Channel) args[1].DataValue;
            CurrentVolume = (System.UInt16) args[2].DataValue;
            return;
        }
        public void GetVolume(System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel)
        {
            GetVolume(InstanceID, Channel, null, null);
        }
        public void GetVolume(System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel, object _Tag, Delegate_OnResult_GetVolume _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[3];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           switch(Channel)
           {
               case Enum_A_ARG_TYPE_Channel.MASTER:
                   args[1] = new UPnPArgument("Channel", "Master");
                   break;
               default:
                  args[1] = new UPnPArgument("Channel", GetUnspecifiedValue("Enum_A_ARG_TYPE_Channel"));
                  break;
           }
           args[2] = new UPnPArgument("CurrentVolume", "");
           _S.InvokeAsync("GetVolume", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_GetVolume), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_GetVolume));
        }
        private void Sink_GetVolume(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            for(int i=0;i<Args.Length;++i)
            {
                switch(Args[i].Name)
                {
                    case "Channel":
                        switch((string)Args[i].DataValue)
                        {
                            case "Master":
                                Args[i].DataValue = Enum_A_ARG_TYPE_Channel.MASTER;
                                break;
                            default:
                               SetUnspecifiedValue("Enum_A_ARG_TYPE_Channel", (string)Args[i].DataValue);
                               Args[i].DataValue = Enum_A_ARG_TYPE_Channel._UNSPECIFIED_;
                               break;
                        }
                        break;
                }
            }
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_GetVolume)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.UInt16 )Args[2].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_GetVolume_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.UInt16 )Args[2].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_GetVolume(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            for(int i=0;i<Args.Length;++i)
            {
                switch(Args[i].Name)
                {
                    case "Channel":
                        switch((string)Args[i].DataValue)
                        {
                            case "Master":
                                Args[i].DataValue = Enum_A_ARG_TYPE_Channel.MASTER;
                                break;
                        }
                        break;
                }
            }
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_GetVolume)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.UInt16)UPnPService.CreateObjectInstance(typeof(System.UInt16),null), e, StateInfo[0]);
            }
            else
            {
                OnResult_GetVolume_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.UInt16)UPnPService.CreateObjectInstance(typeof(System.UInt16),null), e, StateInfo[0]);
            }
        }
        public void Sync_GetVolumeDB(System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel, out System.Int16 CurrentVolume)
        {
           UPnPArgument[] args = new UPnPArgument[3];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           switch(Channel)
           {
               case Enum_A_ARG_TYPE_Channel.MASTER:
                   args[1] = new UPnPArgument("Channel", "Master");
                   break;
               default:
                  args[1] = new UPnPArgument("Channel", GetUnspecifiedValue("Enum_A_ARG_TYPE_Channel"));
                  break;
           }
           args[2] = new UPnPArgument("CurrentVolume", "");
            _S.InvokeSync("GetVolumeDB", args);
            for(int i=0;i<args.Length;++i)
            {
                switch(args[i].Name)
                {
                    case "Channel":
                        switch((string)args[i].DataValue)
                        {
                            case "Master":
                                args[i].DataValue = Enum_A_ARG_TYPE_Channel.MASTER;
                                break;
                            default:
                               SetUnspecifiedValue("Enum_A_ARG_TYPE_Channel", (string)args[i].DataValue);
                               args[i].DataValue = Enum_A_ARG_TYPE_Channel._UNSPECIFIED_;
                               break;
                        }
                        break;
                }
            }
            InstanceID = (System.UInt32) args[0].DataValue;
            Channel = (Enum_A_ARG_TYPE_Channel) args[1].DataValue;
            CurrentVolume = (System.Int16) args[2].DataValue;
            return;
        }
        public void GetVolumeDB(System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel)
        {
            GetVolumeDB(InstanceID, Channel, null, null);
        }
        public void GetVolumeDB(System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel, object _Tag, Delegate_OnResult_GetVolumeDB _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[3];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           switch(Channel)
           {
               case Enum_A_ARG_TYPE_Channel.MASTER:
                   args[1] = new UPnPArgument("Channel", "Master");
                   break;
               default:
                  args[1] = new UPnPArgument("Channel", GetUnspecifiedValue("Enum_A_ARG_TYPE_Channel"));
                  break;
           }
           args[2] = new UPnPArgument("CurrentVolume", "");
           _S.InvokeAsync("GetVolumeDB", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_GetVolumeDB), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_GetVolumeDB));
        }
        private void Sink_GetVolumeDB(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            for(int i=0;i<Args.Length;++i)
            {
                switch(Args[i].Name)
                {
                    case "Channel":
                        switch((string)Args[i].DataValue)
                        {
                            case "Master":
                                Args[i].DataValue = Enum_A_ARG_TYPE_Channel.MASTER;
                                break;
                            default:
                               SetUnspecifiedValue("Enum_A_ARG_TYPE_Channel", (string)Args[i].DataValue);
                               Args[i].DataValue = Enum_A_ARG_TYPE_Channel._UNSPECIFIED_;
                               break;
                        }
                        break;
                }
            }
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_GetVolumeDB)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.Int16 )Args[2].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_GetVolumeDB_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.Int16 )Args[2].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_GetVolumeDB(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            for(int i=0;i<Args.Length;++i)
            {
                switch(Args[i].Name)
                {
                    case "Channel":
                        switch((string)Args[i].DataValue)
                        {
                            case "Master":
                                Args[i].DataValue = Enum_A_ARG_TYPE_Channel.MASTER;
                                break;
                        }
                        break;
                }
            }
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_GetVolumeDB)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.Int16)UPnPService.CreateObjectInstance(typeof(System.Int16),null), e, StateInfo[0]);
            }
            else
            {
                OnResult_GetVolumeDB_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.Int16)UPnPService.CreateObjectInstance(typeof(System.Int16),null), e, StateInfo[0]);
            }
        }
        public void Sync_GetVolumeDBRange(System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel, out System.Int16 MinValue, out System.Int16 MaxValue)
        {
           UPnPArgument[] args = new UPnPArgument[4];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           switch(Channel)
           {
               case Enum_A_ARG_TYPE_Channel.MASTER:
                   args[1] = new UPnPArgument("Channel", "Master");
                   break;
               default:
                  args[1] = new UPnPArgument("Channel", GetUnspecifiedValue("Enum_A_ARG_TYPE_Channel"));
                  break;
           }
           args[2] = new UPnPArgument("MinValue", "");
           args[3] = new UPnPArgument("MaxValue", "");
            _S.InvokeSync("GetVolumeDBRange", args);
            for(int i=0;i<args.Length;++i)
            {
                switch(args[i].Name)
                {
                    case "Channel":
                        switch((string)args[i].DataValue)
                        {
                            case "Master":
                                args[i].DataValue = Enum_A_ARG_TYPE_Channel.MASTER;
                                break;
                            default:
                               SetUnspecifiedValue("Enum_A_ARG_TYPE_Channel", (string)args[i].DataValue);
                               args[i].DataValue = Enum_A_ARG_TYPE_Channel._UNSPECIFIED_;
                               break;
                        }
                        break;
                }
            }
            InstanceID = (System.UInt32) args[0].DataValue;
            Channel = (Enum_A_ARG_TYPE_Channel) args[1].DataValue;
            MinValue = (System.Int16) args[2].DataValue;
            MaxValue = (System.Int16) args[3].DataValue;
            return;
        }
        public void GetVolumeDBRange(System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel)
        {
            GetVolumeDBRange(InstanceID, Channel, null, null);
        }
        public void GetVolumeDBRange(System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel, object _Tag, Delegate_OnResult_GetVolumeDBRange _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[4];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           switch(Channel)
           {
               case Enum_A_ARG_TYPE_Channel.MASTER:
                   args[1] = new UPnPArgument("Channel", "Master");
                   break;
               default:
                  args[1] = new UPnPArgument("Channel", GetUnspecifiedValue("Enum_A_ARG_TYPE_Channel"));
                  break;
           }
           args[2] = new UPnPArgument("MinValue", "");
           args[3] = new UPnPArgument("MaxValue", "");
           _S.InvokeAsync("GetVolumeDBRange", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_GetVolumeDBRange), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_GetVolumeDBRange));
        }
        private void Sink_GetVolumeDBRange(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            for(int i=0;i<Args.Length;++i)
            {
                switch(Args[i].Name)
                {
                    case "Channel":
                        switch((string)Args[i].DataValue)
                        {
                            case "Master":
                                Args[i].DataValue = Enum_A_ARG_TYPE_Channel.MASTER;
                                break;
                            default:
                               SetUnspecifiedValue("Enum_A_ARG_TYPE_Channel", (string)Args[i].DataValue);
                               Args[i].DataValue = Enum_A_ARG_TYPE_Channel._UNSPECIFIED_;
                               break;
                        }
                        break;
                }
            }
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_GetVolumeDBRange)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.Int16 )Args[2].DataValue, (System.Int16 )Args[3].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_GetVolumeDBRange_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.Int16 )Args[2].DataValue, (System.Int16 )Args[3].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_GetVolumeDBRange(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            for(int i=0;i<Args.Length;++i)
            {
                switch(Args[i].Name)
                {
                    case "Channel":
                        switch((string)Args[i].DataValue)
                        {
                            case "Master":
                                Args[i].DataValue = Enum_A_ARG_TYPE_Channel.MASTER;
                                break;
                        }
                        break;
                }
            }
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_GetVolumeDBRange)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.Int16)UPnPService.CreateObjectInstance(typeof(System.Int16),null), (System.Int16)UPnPService.CreateObjectInstance(typeof(System.Int16),null), e, StateInfo[0]);
            }
            else
            {
                OnResult_GetVolumeDBRange_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.Int16)UPnPService.CreateObjectInstance(typeof(System.Int16),null), (System.Int16)UPnPService.CreateObjectInstance(typeof(System.Int16),null), e, StateInfo[0]);
            }
        }
        public void Sync_ListPresets(System.UInt32 InstanceID, out Enum_PresetNameList CurrentPresetNameList)
        {
           UPnPArgument[] args = new UPnPArgument[2];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           args[1] = new UPnPArgument("CurrentPresetNameList", "");
            _S.InvokeSync("ListPresets", args);
            for(int i=0;i<args.Length;++i)
            {
                switch(args[i].Name)
                {
                    case "CurrentPresetNameList":
                        switch((string)args[i].DataValue)
                        {
                            case "FactoryDefaults":
                                args[i].DataValue = Enum_PresetNameList.FACTORYDEFAULTS;
                                break;
                            default:
                               SetUnspecifiedValue("Enum_PresetNameList", (string)args[i].DataValue);
                               args[i].DataValue = Enum_PresetNameList._UNSPECIFIED_;
                               break;
                        }
                        break;
                }
            }
            InstanceID = (System.UInt32) args[0].DataValue;
            CurrentPresetNameList = (Enum_PresetNameList) args[1].DataValue;
            return;
        }
        public void ListPresets(System.UInt32 InstanceID)
        {
            ListPresets(InstanceID, null, null);
        }
        public void ListPresets(System.UInt32 InstanceID, object _Tag, Delegate_OnResult_ListPresets _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[2];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           args[1] = new UPnPArgument("CurrentPresetNameList", "");
           _S.InvokeAsync("ListPresets", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_ListPresets), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_ListPresets));
        }
        private void Sink_ListPresets(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            for(int i=0;i<Args.Length;++i)
            {
                switch(Args[i].Name)
                {
                    case "CurrentPresetNameList":
                        switch((string)Args[i].DataValue)
                        {
                            case "FactoryDefaults":
                                Args[i].DataValue = Enum_PresetNameList.FACTORYDEFAULTS;
                                break;
                            default:
                               SetUnspecifiedValue("Enum_PresetNameList", (string)Args[i].DataValue);
                               Args[i].DataValue = Enum_PresetNameList._UNSPECIFIED_;
                               break;
                        }
                        break;
                }
            }
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_ListPresets)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (Enum_PresetNameList )Args[1].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_ListPresets_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (Enum_PresetNameList )Args[1].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_ListPresets(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_ListPresets)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (Enum_PresetNameList)0, e, StateInfo[0]);
            }
            else
            {
                OnResult_ListPresets_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (Enum_PresetNameList)0, e, StateInfo[0]);
            }
        }
        public void Sync_SelectPreset(System.UInt32 InstanceID, Enum_A_ARG_TYPE_PresetName PresetName)
        {
           UPnPArgument[] args = new UPnPArgument[2];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           switch(PresetName)
           {
               case Enum_A_ARG_TYPE_PresetName.FACTORYDEFAULTS:
                   args[1] = new UPnPArgument("PresetName", "FactoryDefaults");
                   break;
               default:
                  args[1] = new UPnPArgument("PresetName", GetUnspecifiedValue("Enum_A_ARG_TYPE_PresetName"));
                  break;
           }
            _S.InvokeSync("SelectPreset", args);
            for(int i=0;i<args.Length;++i)
            {
                switch(args[i].Name)
                {
                    case "PresetName":
                        switch((string)args[i].DataValue)
                        {
                            case "FactoryDefaults":
                                args[i].DataValue = Enum_A_ARG_TYPE_PresetName.FACTORYDEFAULTS;
                                break;
                            default:
                               SetUnspecifiedValue("Enum_A_ARG_TYPE_PresetName", (string)args[i].DataValue);
                               args[i].DataValue = Enum_A_ARG_TYPE_PresetName._UNSPECIFIED_;
                               break;
                        }
                        break;
                }
            }
            InstanceID = (System.UInt32) args[0].DataValue;
            PresetName = (Enum_A_ARG_TYPE_PresetName) args[1].DataValue;
            return;
        }
        public void SelectPreset(System.UInt32 InstanceID, Enum_A_ARG_TYPE_PresetName PresetName)
        {
            SelectPreset(InstanceID, PresetName, null, null);
        }
        public void SelectPreset(System.UInt32 InstanceID, Enum_A_ARG_TYPE_PresetName PresetName, object _Tag, Delegate_OnResult_SelectPreset _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[2];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           switch(PresetName)
           {
               case Enum_A_ARG_TYPE_PresetName.FACTORYDEFAULTS:
                   args[1] = new UPnPArgument("PresetName", "FactoryDefaults");
                   break;
               default:
                  args[1] = new UPnPArgument("PresetName", GetUnspecifiedValue("Enum_A_ARG_TYPE_PresetName"));
                  break;
           }
           _S.InvokeAsync("SelectPreset", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_SelectPreset), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_SelectPreset));
        }
        private void Sink_SelectPreset(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            for(int i=0;i<Args.Length;++i)
            {
                switch(Args[i].Name)
                {
                    case "PresetName":
                        switch((string)Args[i].DataValue)
                        {
                            case "FactoryDefaults":
                                Args[i].DataValue = Enum_A_ARG_TYPE_PresetName.FACTORYDEFAULTS;
                                break;
                            default:
                               SetUnspecifiedValue("Enum_A_ARG_TYPE_PresetName", (string)Args[i].DataValue);
                               Args[i].DataValue = Enum_A_ARG_TYPE_PresetName._UNSPECIFIED_;
                               break;
                        }
                        break;
                }
            }
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_SelectPreset)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_PresetName )Args[1].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_SelectPreset_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_PresetName )Args[1].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_SelectPreset(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            for(int i=0;i<Args.Length;++i)
            {
                switch(Args[i].Name)
                {
                    case "PresetName":
                        switch((string)Args[i].DataValue)
                        {
                            case "FactoryDefaults":
                                Args[i].DataValue = Enum_A_ARG_TYPE_PresetName.FACTORYDEFAULTS;
                                break;
                        }
                        break;
                }
            }
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_SelectPreset)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_PresetName )Args[1].DataValue, e, StateInfo[0]);
            }
            else
            {
                OnResult_SelectPreset_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_PresetName )Args[1].DataValue, e, StateInfo[0]);
            }
        }
        public void Sync_SetMute(System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel, System.Boolean DesiredMute)
        {
           UPnPArgument[] args = new UPnPArgument[3];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           switch(Channel)
           {
               case Enum_A_ARG_TYPE_Channel.MASTER:
                   args[1] = new UPnPArgument("Channel", "Master");
                   break;
               default:
                  args[1] = new UPnPArgument("Channel", GetUnspecifiedValue("Enum_A_ARG_TYPE_Channel"));
                  break;
           }
           args[2] = new UPnPArgument("DesiredMute", DesiredMute);
            _S.InvokeSync("SetMute", args);
            for(int i=0;i<args.Length;++i)
            {
                switch(args[i].Name)
                {
                    case "Channel":
                        switch((string)args[i].DataValue)
                        {
                            case "Master":
                                args[i].DataValue = Enum_A_ARG_TYPE_Channel.MASTER;
                                break;
                            default:
                               SetUnspecifiedValue("Enum_A_ARG_TYPE_Channel", (string)args[i].DataValue);
                               args[i].DataValue = Enum_A_ARG_TYPE_Channel._UNSPECIFIED_;
                               break;
                        }
                        break;
                }
            }
            InstanceID = (System.UInt32) args[0].DataValue;
            Channel = (Enum_A_ARG_TYPE_Channel) args[1].DataValue;
            DesiredMute = (System.Boolean) args[2].DataValue;
            return;
        }
        public void SetMute(System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel, System.Boolean DesiredMute)
        {
            SetMute(InstanceID, Channel, DesiredMute, null, null);
        }
        public void SetMute(System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel, System.Boolean DesiredMute, object _Tag, Delegate_OnResult_SetMute _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[3];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           switch(Channel)
           {
               case Enum_A_ARG_TYPE_Channel.MASTER:
                   args[1] = new UPnPArgument("Channel", "Master");
                   break;
               default:
                  args[1] = new UPnPArgument("Channel", GetUnspecifiedValue("Enum_A_ARG_TYPE_Channel"));
                  break;
           }
           args[2] = new UPnPArgument("DesiredMute", DesiredMute);
           _S.InvokeAsync("SetMute", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_SetMute), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_SetMute));
        }
        private void Sink_SetMute(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            for(int i=0;i<Args.Length;++i)
            {
                switch(Args[i].Name)
                {
                    case "Channel":
                        switch((string)Args[i].DataValue)
                        {
                            case "Master":
                                Args[i].DataValue = Enum_A_ARG_TYPE_Channel.MASTER;
                                break;
                            default:
                               SetUnspecifiedValue("Enum_A_ARG_TYPE_Channel", (string)Args[i].DataValue);
                               Args[i].DataValue = Enum_A_ARG_TYPE_Channel._UNSPECIFIED_;
                               break;
                        }
                        break;
                }
            }
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_SetMute)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.Boolean )Args[2].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_SetMute_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.Boolean )Args[2].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_SetMute(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            for(int i=0;i<Args.Length;++i)
            {
                switch(Args[i].Name)
                {
                    case "Channel":
                        switch((string)Args[i].DataValue)
                        {
                            case "Master":
                                Args[i].DataValue = Enum_A_ARG_TYPE_Channel.MASTER;
                                break;
                        }
                        break;
                }
            }
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_SetMute)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.Boolean )Args[2].DataValue, e, StateInfo[0]);
            }
            else
            {
                OnResult_SetMute_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.Boolean )Args[2].DataValue, e, StateInfo[0]);
            }
        }
        public void Sync_SetVolume(System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel, System.UInt16 DesiredVolume)
        {
           UPnPArgument[] args = new UPnPArgument[3];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           switch(Channel)
           {
               case Enum_A_ARG_TYPE_Channel.MASTER:
                   args[1] = new UPnPArgument("Channel", "Master");
                   break;
               default:
                  args[1] = new UPnPArgument("Channel", GetUnspecifiedValue("Enum_A_ARG_TYPE_Channel"));
                  break;
           }
           args[2] = new UPnPArgument("DesiredVolume", DesiredVolume);
            _S.InvokeSync("SetVolume", args);
            for(int i=0;i<args.Length;++i)
            {
                switch(args[i].Name)
                {
                    case "Channel":
                        switch((string)args[i].DataValue)
                        {
                            case "Master":
                                args[i].DataValue = Enum_A_ARG_TYPE_Channel.MASTER;
                                break;
                            default:
                               SetUnspecifiedValue("Enum_A_ARG_TYPE_Channel", (string)args[i].DataValue);
                               args[i].DataValue = Enum_A_ARG_TYPE_Channel._UNSPECIFIED_;
                               break;
                        }
                        break;
                }
            }
            InstanceID = (System.UInt32) args[0].DataValue;
            Channel = (Enum_A_ARG_TYPE_Channel) args[1].DataValue;
            DesiredVolume = (System.UInt16) args[2].DataValue;
            return;
        }
        public void SetVolume(System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel, System.UInt16 DesiredVolume)
        {
            SetVolume(InstanceID, Channel, DesiredVolume, null, null);
        }
        public void SetVolume(System.UInt32 InstanceID, Enum_A_ARG_TYPE_Channel Channel, System.UInt16 DesiredVolume, object _Tag, Delegate_OnResult_SetVolume _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[3];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           switch(Channel)
           {
               case Enum_A_ARG_TYPE_Channel.MASTER:
                   args[1] = new UPnPArgument("Channel", "Master");
                   break;
               default:
                  args[1] = new UPnPArgument("Channel", GetUnspecifiedValue("Enum_A_ARG_TYPE_Channel"));
                  break;
           }
           args[2] = new UPnPArgument("DesiredVolume", DesiredVolume);
           _S.InvokeAsync("SetVolume", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_SetVolume), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_SetVolume));
        }
        private void Sink_SetVolume(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            for(int i=0;i<Args.Length;++i)
            {
                switch(Args[i].Name)
                {
                    case "Channel":
                        switch((string)Args[i].DataValue)
                        {
                            case "Master":
                                Args[i].DataValue = Enum_A_ARG_TYPE_Channel.MASTER;
                                break;
                            default:
                               SetUnspecifiedValue("Enum_A_ARG_TYPE_Channel", (string)Args[i].DataValue);
                               Args[i].DataValue = Enum_A_ARG_TYPE_Channel._UNSPECIFIED_;
                               break;
                        }
                        break;
                }
            }
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_SetVolume)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.UInt16 )Args[2].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_SetVolume_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.UInt16 )Args[2].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_SetVolume(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            for(int i=0;i<Args.Length;++i)
            {
                switch(Args[i].Name)
                {
                    case "Channel":
                        switch((string)Args[i].DataValue)
                        {
                            case "Master":
                                Args[i].DataValue = Enum_A_ARG_TYPE_Channel.MASTER;
                                break;
                        }
                        break;
                }
            }
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_SetVolume)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.UInt16 )Args[2].DataValue, e, StateInfo[0]);
            }
            else
            {
                OnResult_SetVolume_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (Enum_A_ARG_TYPE_Channel )Args[1].DataValue, (System.UInt16 )Args[2].DataValue, e, StateInfo[0]);
            }
        }
    }
}