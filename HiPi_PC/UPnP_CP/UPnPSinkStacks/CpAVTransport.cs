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
    public class CpAVTransport
    {
       private Hashtable UnspecifiedTable = Hashtable.Synchronized(new Hashtable());
       internal UPnPService _S;

       public UPnPService GetUPnPService()
       {
            return(_S);
       }
       public static string SERVICE_NAME = "urn:schemas-upnp-org:service:AVTransport:";
       public double VERSION
       {
           get
           {
               return(double.Parse(_S.Version));
           }
       }

       public delegate void StateVariableModifiedHandler_TransportStatus(CpAVTransport sender, System.String NewValue);
       private WeakEvent OnStateVariable_TransportStatus_Event = new WeakEvent();
       public event StateVariableModifiedHandler_TransportStatus OnStateVariable_TransportStatus
       {
			add{OnStateVariable_TransportStatus_Event.Register(value);}
			remove{OnStateVariable_TransportStatus_Event.UnRegister(value);}
       }
       protected void TransportStatus_ModifiedSink(UPnPStateVariable Var, object NewValue)
       {
            OnStateVariable_TransportStatus_Event.Fire(this, TransportStatus);
       }
       public delegate void StateVariableModifiedHandler_TransportState(CpAVTransport sender, System.String NewValue);
       private WeakEvent OnStateVariable_TransportState_Event = new WeakEvent();
       public event StateVariableModifiedHandler_TransportState OnStateVariable_TransportState
       {
			add{OnStateVariable_TransportState_Event.Register(value);}
			remove{OnStateVariable_TransportState_Event.UnRegister(value);}
       }
       protected void TransportState_ModifiedSink(UPnPStateVariable Var, object NewValue)
       {
            OnStateVariable_TransportState_Event.Fire(this, TransportState);
       }
       public delegate void SubscribeHandler(CpAVTransport sender, bool Success);
       public event SubscribeHandler OnSubscribe;
       public delegate void Delegate_OnResult_GetTransportInfo(CpAVTransport sender, System.UInt32 InstanceID, System.String CurrentTransportState, System.String CurrentTransportStatus, System.String CurrentSpeed, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_GetTransportInfo_Event = new WeakEvent();
       public event Delegate_OnResult_GetTransportInfo OnResult_GetTransportInfo
       {
			add{OnResult_GetTransportInfo_Event.Register(value);}
			remove{OnResult_GetTransportInfo_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_Next(CpAVTransport sender, System.UInt32 InstanceID, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_Next_Event = new WeakEvent();
       public event Delegate_OnResult_Next OnResult_Next
       {
			add{OnResult_Next_Event.Register(value);}
			remove{OnResult_Next_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_Pause(CpAVTransport sender, System.UInt32 InstanceID, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_Pause_Event = new WeakEvent();
       public event Delegate_OnResult_Pause OnResult_Pause
       {
			add{OnResult_Pause_Event.Register(value);}
			remove{OnResult_Pause_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_Play(CpAVTransport sender, System.UInt32 InstanceID, System.String Speed, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_Play_Event = new WeakEvent();
       public event Delegate_OnResult_Play OnResult_Play
       {
			add{OnResult_Play_Event.Register(value);}
			remove{OnResult_Play_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_Previous(CpAVTransport sender, System.UInt32 InstanceID, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_Previous_Event = new WeakEvent();
       public event Delegate_OnResult_Previous OnResult_Previous
       {
			add{OnResult_Previous_Event.Register(value);}
			remove{OnResult_Previous_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_SetAVTransportURI(CpAVTransport sender, System.UInt32 InstanceID, System.String CurrentURI, System.String CurrentURIMetaData, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_SetAVTransportURI_Event = new WeakEvent();
       public event Delegate_OnResult_SetAVTransportURI OnResult_SetAVTransportURI
       {
			add{OnResult_SetAVTransportURI_Event.Register(value);}
			remove{OnResult_SetAVTransportURI_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_SetNextAVTransportURI(CpAVTransport sender, System.UInt32 InstanceID, System.String NextURI, System.String NextURIMetaData, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_SetNextAVTransportURI_Event = new WeakEvent();
       public event Delegate_OnResult_SetNextAVTransportURI OnResult_SetNextAVTransportURI
       {
			add{OnResult_SetNextAVTransportURI_Event.Register(value);}
			remove{OnResult_SetNextAVTransportURI_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_Stop(CpAVTransport sender, System.UInt32 InstanceID, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_Stop_Event = new WeakEvent();
       public event Delegate_OnResult_Stop OnResult_Stop
       {
			add{OnResult_Stop_Event.Register(value);}
			remove{OnResult_Stop_Event.UnRegister(value);}
       }

        public CpAVTransport(UPnPService s)
        {
            _S = s;
            _S.OnSubscribe += new UPnPService.UPnPEventSubscribeHandler(_subscribe_sink);
            if (HasStateVariable_TransportStatus) _S.GetStateVariableObject("TransportStatus").OnModified += new UPnPStateVariable.ModifiedHandler(TransportStatus_ModifiedSink);
            if (HasStateVariable_TransportState) _S.GetStateVariableObject("TransportState").OnModified += new UPnPStateVariable.ModifiedHandler(TransportState_ModifiedSink);
        }
        public void Dispose()
        {
            _S.OnSubscribe -= new UPnPService.UPnPEventSubscribeHandler(_subscribe_sink);
            OnSubscribe = null;
            if (HasStateVariable_TransportStatus) _S.GetStateVariableObject("TransportStatus").OnModified -= new UPnPStateVariable.ModifiedHandler(TransportStatus_ModifiedSink);
            if (HasStateVariable_TransportState) _S.GetStateVariableObject("TransportState").OnModified -= new UPnPStateVariable.ModifiedHandler(TransportState_ModifiedSink);
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
        public System.String AVTransportURI
        {
            get
            {
               return((System.String)_S.GetStateVariable("AVTransportURI"));
            }
        }
        public System.UInt32 A_ARG_TYPE_InstanceID
        {
            get
            {
               return((System.UInt32)_S.GetStateVariable("A_ARG_TYPE_InstanceID"));
            }
        }
        public System.String NextAVTransportURI
        {
            get
            {
               return((System.String)_S.GetStateVariable("NextAVTransportURI"));
            }
        }
        public System.String TransportStatus
        {
            get
            {
               return((System.String)_S.GetStateVariable("TransportStatus"));
            }
        }
        public System.String TransportState
        {
            get
            {
               return((System.String)_S.GetStateVariable("TransportState"));
            }
        }
        public System.String A_ARG_TYPE_Speed
        {
            get
            {
               return((System.String)_S.GetStateVariable("A_ARG_TYPE_Speed"));
            }
        }
        public System.String NextAVTransportURIMetaData
        {
            get
            {
               return((System.String)_S.GetStateVariable("NextAVTransportURIMetaData"));
            }
        }
        public System.String AVTransportURIMetaData
        {
            get
            {
               return((System.String)_S.GetStateVariable("AVTransportURIMetaData"));
            }
        }
        public System.String TransportPlaySpeed
        {
            get
            {
               return((System.String)_S.GetStateVariable("TransportPlaySpeed"));
            }
        }
        public bool HasStateVariable_AVTransportURI
        {
            get
            {
               if (_S.GetStateVariableObject("AVTransportURI")==null)
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
        public bool HasStateVariable_NextAVTransportURI
        {
            get
            {
               if (_S.GetStateVariableObject("NextAVTransportURI")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_TransportStatus
        {
            get
            {
               if (_S.GetStateVariableObject("TransportStatus")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_TransportState
        {
            get
            {
               if (_S.GetStateVariableObject("TransportState")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_A_ARG_TYPE_Speed
        {
            get
            {
               if (_S.GetStateVariableObject("A_ARG_TYPE_Speed")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_NextAVTransportURIMetaData
        {
            get
            {
               if (_S.GetStateVariableObject("NextAVTransportURIMetaData")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_AVTransportURIMetaData
        {
            get
            {
               if (_S.GetStateVariableObject("AVTransportURIMetaData")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_TransportPlaySpeed
        {
            get
            {
               if (_S.GetStateVariableObject("TransportPlaySpeed")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_GetTransportInfo
        {
            get
            {
               if (_S.GetAction("GetTransportInfo")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_Next
        {
            get
            {
               if (_S.GetAction("Next")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_Pause
        {
            get
            {
               if (_S.GetAction("Pause")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_Play
        {
            get
            {
               if (_S.GetAction("Play")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_Previous
        {
            get
            {
               if (_S.GetAction("Previous")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_SetAVTransportURI
        {
            get
            {
               if (_S.GetAction("SetAVTransportURI")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_SetNextAVTransportURI
        {
            get
            {
               if (_S.GetAction("SetNextAVTransportURI")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_Stop
        {
            get
            {
               if (_S.GetAction("Stop")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public void Sync_GetTransportInfo(System.UInt32 InstanceID, out System.String CurrentTransportState, out System.String CurrentTransportStatus, out System.String CurrentSpeed)
        {
           UPnPArgument[] args = new UPnPArgument[4];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           args[1] = new UPnPArgument("CurrentTransportState", "");
           args[2] = new UPnPArgument("CurrentTransportStatus", "");
           args[3] = new UPnPArgument("CurrentSpeed", "");
            _S.InvokeSync("GetTransportInfo", args);
            InstanceID = (System.UInt32) args[0].DataValue;
            CurrentTransportState = (System.String) args[1].DataValue;
            CurrentTransportStatus = (System.String) args[2].DataValue;
            CurrentSpeed = (System.String) args[3].DataValue;
            return;
        }
        public void GetTransportInfo(System.UInt32 InstanceID)
        {
            GetTransportInfo(InstanceID, null, null);
        }
        public void GetTransportInfo(System.UInt32 InstanceID, object _Tag, Delegate_OnResult_GetTransportInfo _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[4];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           args[1] = new UPnPArgument("CurrentTransportState", "");
           args[2] = new UPnPArgument("CurrentTransportStatus", "");
           args[3] = new UPnPArgument("CurrentSpeed", "");
           _S.InvokeAsync("GetTransportInfo", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_GetTransportInfo), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_GetTransportInfo));
        }
        private void Sink_GetTransportInfo(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_GetTransportInfo)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, (System.String )Args[2].DataValue, (System.String )Args[3].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_GetTransportInfo_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, (System.String )Args[2].DataValue, (System.String )Args[3].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_GetTransportInfo(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_GetTransportInfo)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (System.String)UPnPService.CreateObjectInstance(typeof(System.String),null), (System.String)UPnPService.CreateObjectInstance(typeof(System.String),null), (System.String)UPnPService.CreateObjectInstance(typeof(System.String),null), e, StateInfo[0]);
            }
            else
            {
                OnResult_GetTransportInfo_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (System.String)UPnPService.CreateObjectInstance(typeof(System.String),null), (System.String)UPnPService.CreateObjectInstance(typeof(System.String),null), (System.String)UPnPService.CreateObjectInstance(typeof(System.String),null), e, StateInfo[0]);
            }
        }
        public void Sync_Next(System.UInt32 InstanceID)
        {
           UPnPArgument[] args = new UPnPArgument[1];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
            _S.InvokeSync("Next", args);
            InstanceID = (System.UInt32) args[0].DataValue;
            return;
        }
        public void Next(System.UInt32 InstanceID)
        {
            Next(InstanceID, null, null);
        }
        public void Next(System.UInt32 InstanceID, object _Tag, Delegate_OnResult_Next _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[1];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           _S.InvokeAsync("Next", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_Next), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_Next));
        }
        private void Sink_Next(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_Next)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_Next_Event.Fire(this, (System.UInt32 )Args[0].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_Next(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_Next)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, e, StateInfo[0]);
            }
            else
            {
                OnResult_Next_Event.Fire(this, (System.UInt32 )Args[0].DataValue, e, StateInfo[0]);
            }
        }
        public void Sync_Pause(System.UInt32 InstanceID)
        {
           UPnPArgument[] args = new UPnPArgument[1];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
            _S.InvokeSync("Pause", args);
            InstanceID = (System.UInt32) args[0].DataValue;
            return;
        }
        public void Pause(System.UInt32 InstanceID)
        {
            Pause(InstanceID, null, null);
        }
        public void Pause(System.UInt32 InstanceID, object _Tag, Delegate_OnResult_Pause _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[1];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           _S.InvokeAsync("Pause", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_Pause), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_Pause));
        }
        private void Sink_Pause(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_Pause)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_Pause_Event.Fire(this, (System.UInt32 )Args[0].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_Pause(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_Pause)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, e, StateInfo[0]);
            }
            else
            {
                OnResult_Pause_Event.Fire(this, (System.UInt32 )Args[0].DataValue, e, StateInfo[0]);
            }
        }
        public void Sync_Play(System.UInt32 InstanceID, System.String Speed)
        {
           UPnPArgument[] args = new UPnPArgument[2];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           args[1] = new UPnPArgument("Speed", Speed);
            _S.InvokeSync("Play", args);
            InstanceID = (System.UInt32) args[0].DataValue;
            Speed = (System.String) args[1].DataValue;
            return;
        }
        public void Play(System.UInt32 InstanceID, System.String Speed)
        {
            Play(InstanceID, Speed, null, null);
        }
        public void Play(System.UInt32 InstanceID, System.String Speed, object _Tag, Delegate_OnResult_Play _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[2];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           args[1] = new UPnPArgument("Speed", Speed);
           _S.InvokeAsync("Play", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_Play), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_Play));
        }
        private void Sink_Play(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_Play)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_Play_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_Play(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_Play)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, e, StateInfo[0]);
            }
            else
            {
                OnResult_Play_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, e, StateInfo[0]);
            }
        }
        public void Sync_Previous(System.UInt32 InstanceID)
        {
           UPnPArgument[] args = new UPnPArgument[1];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
            _S.InvokeSync("Previous", args);
            InstanceID = (System.UInt32) args[0].DataValue;
            return;
        }
        public void Previous(System.UInt32 InstanceID)
        {
            Previous(InstanceID, null, null);
        }
        public void Previous(System.UInt32 InstanceID, object _Tag, Delegate_OnResult_Previous _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[1];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           _S.InvokeAsync("Previous", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_Previous), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_Previous));
        }
        private void Sink_Previous(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_Previous)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_Previous_Event.Fire(this, (System.UInt32 )Args[0].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_Previous(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_Previous)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, e, StateInfo[0]);
            }
            else
            {
                OnResult_Previous_Event.Fire(this, (System.UInt32 )Args[0].DataValue, e, StateInfo[0]);
            }
        }
        public void Sync_SetAVTransportURI(System.UInt32 InstanceID, System.String CurrentURI, System.String CurrentURIMetaData)
        {
           UPnPArgument[] args = new UPnPArgument[3];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           args[1] = new UPnPArgument("CurrentURI", CurrentURI);
           args[2] = new UPnPArgument("CurrentURIMetaData", CurrentURIMetaData);
            _S.InvokeSync("SetAVTransportURI", args);
            InstanceID = (System.UInt32) args[0].DataValue;
            CurrentURI = (System.String) args[1].DataValue;
            CurrentURIMetaData = (System.String) args[2].DataValue;
            return;
        }
        public void SetAVTransportURI(System.UInt32 InstanceID, System.String CurrentURI, System.String CurrentURIMetaData)
        {
            SetAVTransportURI(InstanceID, CurrentURI, CurrentURIMetaData, null, null);
        }
        public void SetAVTransportURI(System.UInt32 InstanceID, System.String CurrentURI, System.String CurrentURIMetaData, object _Tag, Delegate_OnResult_SetAVTransportURI _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[3];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           args[1] = new UPnPArgument("CurrentURI", CurrentURI);
           args[2] = new UPnPArgument("CurrentURIMetaData", CurrentURIMetaData);
           _S.InvokeAsync("SetAVTransportURI", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_SetAVTransportURI), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_SetAVTransportURI));
        }
        private void Sink_SetAVTransportURI(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_SetAVTransportURI)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, (System.String )Args[2].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_SetAVTransportURI_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, (System.String )Args[2].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_SetAVTransportURI(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_SetAVTransportURI)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, (System.String )Args[2].DataValue, e, StateInfo[0]);
            }
            else
            {
                OnResult_SetAVTransportURI_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, (System.String )Args[2].DataValue, e, StateInfo[0]);
            }
        }
        public void Sync_SetNextAVTransportURI(System.UInt32 InstanceID, System.String NextURI, System.String NextURIMetaData)
        {
           UPnPArgument[] args = new UPnPArgument[3];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           args[1] = new UPnPArgument("NextURI", NextURI);
           args[2] = new UPnPArgument("NextURIMetaData", NextURIMetaData);
            _S.InvokeSync("SetNextAVTransportURI", args);
            InstanceID = (System.UInt32) args[0].DataValue;
            NextURI = (System.String) args[1].DataValue;
            NextURIMetaData = (System.String) args[2].DataValue;
            return;
        }
        public void SetNextAVTransportURI(System.UInt32 InstanceID, System.String NextURI, System.String NextURIMetaData)
        {
            SetNextAVTransportURI(InstanceID, NextURI, NextURIMetaData, null, null);
        }
        public void SetNextAVTransportURI(System.UInt32 InstanceID, System.String NextURI, System.String NextURIMetaData, object _Tag, Delegate_OnResult_SetNextAVTransportURI _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[3];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           args[1] = new UPnPArgument("NextURI", NextURI);
           args[2] = new UPnPArgument("NextURIMetaData", NextURIMetaData);
           _S.InvokeAsync("SetNextAVTransportURI", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_SetNextAVTransportURI), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_SetNextAVTransportURI));
        }
        private void Sink_SetNextAVTransportURI(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_SetNextAVTransportURI)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, (System.String )Args[2].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_SetNextAVTransportURI_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, (System.String )Args[2].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_SetNextAVTransportURI(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_SetNextAVTransportURI)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, (System.String )Args[2].DataValue, e, StateInfo[0]);
            }
            else
            {
                OnResult_SetNextAVTransportURI_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, (System.String )Args[2].DataValue, e, StateInfo[0]);
            }
        }
        public void Sync_Stop(System.UInt32 InstanceID)
        {
           UPnPArgument[] args = new UPnPArgument[1];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
            _S.InvokeSync("Stop", args);
            InstanceID = (System.UInt32) args[0].DataValue;
            return;
        }
        public void Stop(System.UInt32 InstanceID)
        {
            Stop(InstanceID, null, null);
        }
        public void Stop(System.UInt32 InstanceID, object _Tag, Delegate_OnResult_Stop _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[1];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           _S.InvokeAsync("Stop", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_Stop), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_Stop));
        }
        private void Sink_Stop(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_Stop)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_Stop_Event.Fire(this, (System.UInt32 )Args[0].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_Stop(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_Stop)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, e, StateInfo[0]);
            }
            else
            {
                OnResult_Stop_Event.Fire(this, (System.UInt32 )Args[0].DataValue, e, StateInfo[0]);
            }
        }
    }
}