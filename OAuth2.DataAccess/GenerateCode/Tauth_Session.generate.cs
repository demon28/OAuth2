   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tauth_Session.generate.cs
 * CreateTime : 2017-09-11 16:46:34
 * CodeGenerateVersion : 1.0.0.0
 * TemplateVersion: 2.0.0
 * E_Mail : zhj.pavel@gmail.com
 * Blog : 
 * Copyright (C) YXH
 * 
 ***************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Winner.Framework.Core.DataAccess.Oracle;
using OAuth2.Entities;

namespace OAuth2.DataAccess
{
	/// <summary>
	/// 当前用户登录会话
	/// </summary>
	public partial class Tauth_Session : DataAccessBase
	{
		#region 构造和基本
		public Tauth_Session():base()
		{}
		public Tauth_Session(DataRow dataRow):base(dataRow)
		{}
		public const string _CLIENT_VERSION = "CLIENT_VERSION";
		public const string _LOGIN_ID = "LOGIN_ID";
		public const string _USER_ID = "USER_ID";
		public const string _SESSION_ID = "SESSION_ID";
		public const string _TOKEN = "TOKEN";
		public const string _CREATE_TIME = "CREATE_TIME";
		public const string _DEVICE_ID = "DEVICE_ID";
		public const string _IP_ADDRESS = "IP_ADDRESS";
		public const string _CLIENT_SYSTEM = "CLIENT_SYSTEM";
		public const string _CLIENT_SOURCE = "CLIENT_SOURCE";
		public const string _STATUS = "STATUS";
		public const string _TableName = "TAUTH_SESSION";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TAUTH_SESSION");
			table.Columns.Add(_CLIENT_VERSION,typeof(string)).DefaultValue=DBNull.Value;
			table.Columns.Add(_LOGIN_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_USER_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_SESSION_ID,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_TOKEN,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_CREATE_TIME,typeof(DateTime)).DefaultValue=DateTime.Now;
			table.Columns.Add(_DEVICE_ID,typeof(string)).DefaultValue=DBNull.Value;
			table.Columns.Add(_IP_ADDRESS,typeof(string)).DefaultValue=DBNull.Value;
			table.Columns.Add(_CLIENT_SYSTEM,typeof(string)).DefaultValue=DBNull.Value;
			table.Columns.Add(_CLIENT_SOURCE,typeof(int)).DefaultValue=0;
			table.Columns.Add(_STATUS,typeof(int)).DefaultValue=1;
			return table.NewRow();
		}
		#endregion
		
		#region 属性
		protected override string TableName
		{
			get{return _TableName;}
		}
		/// <summary>
		/// 客户端版本号(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 20Byte
		/// </para>
		/// </summary>
		public string Client_Version
		{
			get{ return DataRow[_CLIENT_VERSION].ToString();}
			 set{setProperty(_CLIENT_VERSION, value);}
		}
		/// <summary>
		/// 会话ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int Login_Id
		{
			get{ return Convert.ToInt32(DataRow[_LOGIN_ID]);}
			 set{setProperty(_LOGIN_ID, value);}
		}
		/// <summary>
		/// 用户ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int User_Id
		{
			get{ return Convert.ToInt32(DataRow[_USER_ID]);}
			 set{setProperty(_USER_ID, value);}
		}
		/// <summary>
		/// 会员代码(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 100Byte
		/// </para>
		/// </summary>
		public string Session_Id
		{
			get{ return DataRow[_SESSION_ID].ToString();}
			 set{setProperty(_SESSION_ID, value);}
		}
		/// <summary>
		/// 登录令牌(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 200Byte
		/// </para>
		/// </summary>
		public string Token
		{
			get{ return DataRow[_TOKEN].ToString();}
			 set{setProperty(_TOKEN, value);}
		}
		/// <summary>
		/// 创建时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 7Byte
		/// </para>
		/// </summary>
		public DateTime Create_Time
		{
			get{ return Convert.ToDateTime(DataRow[_CREATE_TIME]);}
			 set{setProperty(_CREATE_TIME, value);}
		}
		/// <summary>
		/// 设备ID(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 100Byte
		/// </para>
		/// </summary>
		public string Device_Id
		{
			get{ return DataRow[_DEVICE_ID].ToString();}
			 set{setProperty(_DEVICE_ID, value);}
		}
		/// <summary>
		/// 登录ID(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 20Byte
		/// </para>
		/// </summary>
		public string Ip_Address
		{
			get{ return DataRow[_IP_ADDRESS].ToString();}
			 set{setProperty(_IP_ADDRESS, value);}
		}
		/// <summary>
		/// 客户端系统(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 100Byte
		/// </para>
		/// </summary>
		public string Client_System
		{
			get{ return DataRow[_CLIENT_SYSTEM].ToString();}
			 set{setProperty(_CLIENT_SYSTEM, value);}
		}
		/// <summary>
		/// 登录来源(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int Client_Source
		{
			get{ return Convert.ToInt32(DataRow[_CLIENT_SOURCE]);}
			 set{setProperty(_CLIENT_SOURCE, value);}
		}
		/// <summary>
		/// 登录状态[1：活跃中，2：正常退出，3：超时退出](必填)
		/// <para>
		/// defaultValue: 1;   Length: 22Byte
		/// </para>
		/// </summary>
		public int Status
		{
			get{ return Convert.ToInt32(DataRow[_STATUS]);}
			 set{setProperty(_STATUS, value);}
		}
		#endregion
		
		#region 基本方法
		protected bool SelectByCondition(string condition)
		{
			string sql = "SELECT CLIENT_VERSION,LOGIN_ID,USER_ID,SESSION_ID,TOKEN,CREATE_TIME,DEVICE_ID,IP_ADDRESS,CLIENT_SYSTEM,CLIENT_SOURCE,STATUS FROM TAUTH_SESSION WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TAUTH_SESSION WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		public bool Delete(int login_id)
		{
			string condition = " LOGIN_ID=:LOGIN_ID";
			AddParameter(_LOGIN_ID,login_id);
			return DeleteByCondition(condition);
		}
		public bool Delete()
		{
			string condition = " LOGIN_ID=:LOGIN_ID";
			AddParameter(_LOGIN_ID,DataRow[_LOGIN_ID]);
			return DeleteByCondition(condition);
		}
				
		public bool Insert()
		{		
			int id = this.Login_Id = GetSequence("SELECT SEQ_TAUTH_SESSION.nextval FROM DUAL");
			string sql = @"INSERT INTO TAUTH_SESSION(CLIENT_VERSION,LOGIN_ID,USER_ID,SESSION_ID,TOKEN,DEVICE_ID,IP_ADDRESS,CLIENT_SYSTEM,CLIENT_SOURCE,STATUS)
			VALUES (:CLIENT_VERSION,:LOGIN_ID,:USER_ID,:SESSION_ID,:TOKEN,:DEVICE_ID,:IP_ADDRESS,:CLIENT_SYSTEM,:CLIENT_SOURCE,:STATUS)";
			AddParameter(_CLIENT_VERSION,DataRow[_CLIENT_VERSION]);
			AddParameter(_LOGIN_ID,DataRow[_LOGIN_ID]);
			AddParameter(_USER_ID,DataRow[_USER_ID]);
			AddParameter(_SESSION_ID,DataRow[_SESSION_ID]);
			AddParameter(_TOKEN,DataRow[_TOKEN]);
			AddParameter(_DEVICE_ID,DataRow[_DEVICE_ID]);
			AddParameter(_IP_ADDRESS,DataRow[_IP_ADDRESS]);
			AddParameter(_CLIENT_SYSTEM,DataRow[_CLIENT_SYSTEM]);
			AddParameter(_CLIENT_SOURCE,DataRow[_CLIENT_SOURCE]);
			AddParameter(_STATUS,DataRow[_STATUS]);
			return InsertBySql(sql);
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tauth_SessionCollection.Field,object> alterDic,Dictionary<Tauth_SessionCollection.Field,object> conditionDic)
		{
			if (alterDic.Count <= 0)
                return false;
            if (conditionDic.Count <= 0)
                return false;
            StringBuilder sql = new StringBuilder();
            sql.Append("update ").Append(_TableName).Append(" set ");
            foreach (Tauth_SessionCollection.Field key in alterDic.Keys)
            {
                object value = alterDic[key];
                string name = key.ToString();
                sql.Append(name).Append("=:").Append(name).Append(",");
                AddParameter(name, value);
            }
            sql.Remove(sql.Length - 1, 1);//移除最后一个逗号
            sql.Append(" where ");
            foreach (Tauth_SessionCollection.Field key in conditionDic.Keys)
            {
                object value = conditionDic[key];
                string name = key.ToString();
				if (alterDic.Keys.Contains(key))
                {
                    name = string.Concat("condition_", key);
                }
                sql.Append(key).Append("=:").Append(name).Append(" and ");
                AddParameter(name, value);
            }
            int len = " and ".Length;
            sql.Remove(sql.Length - len, len);//移除最后一个and
            return UpdateBySql(sql.ToString());
		}
		protected bool UpdateByCondition(string condition)
		{
			ChangePropertys.Remove(_LOGIN_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TAUTH_SESSION SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=:{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE LOGIN_ID=:LOGIN_ID");
			AddParameter(_LOGIN_ID, DataRow[_LOGIN_ID]);			
			if (!string.IsNullOrEmpty(condition))
            {
				sql.AppendLine(" AND " + condition);
			}
			bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
		}	
		public bool SelectByPk(int login_id)
		{
			string condition = " LOGIN_ID=:LOGIN_ID";
			AddParameter(_LOGIN_ID,login_id);
			return SelectByCondition(condition);
		}
		#endregion
	}
	
	public partial class Tauth_SessionCollection : DataAccessCollectionBase
	{
		#region 构造和基本
		public Tauth_SessionCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tauth_Session().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tauth_Session(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tauth_Session._TableName;}
		}
		public Tauth_Session this[int index]
        {
            get { return new Tauth_Session(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Client_Version=0,
			Login_Id=1,
			User_Id=2,
			Session_Id=3,
			Token=4,
			Create_Time=5,
			Device_Id=6,
			Ip_Address=7,
			Client_System=8,
			Client_Source=9,
			Status=10,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT CLIENT_VERSION,LOGIN_ID,USER_ID,SESSION_ID,TOKEN,CREATE_TIME,DEVICE_ID,IP_ADDRESS,CLIENT_SYSTEM,CLIENT_SOURCE,STATUS FROM TAUTH_SESSION WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListAll()
		{
			string condition = " 1=1";
			return ListByCondition(condition);
		}
		#region Linq
		public Tauth_Session Find(Predicate<Tauth_Session> match)
        {
            foreach (Tauth_Session item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tauth_SessionCollection FindAll(Predicate<Tauth_Session> match)
        {
            Tauth_SessionCollection list = new Tauth_SessionCollection();
            foreach (Tauth_Session item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tauth_Session> match)
        {
            foreach (Tauth_Session item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tauth_Session> match)
        {
            BeginTransaction();
            foreach (Tauth_Session item in this)
            {
                item.ReferenceTransactionFrom(Transaction);
                if (!match(item))
                    continue;
                if (!item.Delete())
                {
                    Rollback();
                    return false;
                }
            }
            Commit();
            return true;
        }
		#endregion
		#endregion		
	}
}