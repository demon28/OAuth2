   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tauth_Code.generate.cs
 * CreateTime : 2017-11-23 11:20:25
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
	/// 授权码
	/// </summary>
	public partial class Tauth_Code : DataAccessBase
	{
		#region 构造和基本
		public Tauth_Code():base()
		{}
		public Tauth_Code(DataRow dataRow):base(dataRow)
		{}
		public const string _RIGHT_JSON = "RIGHT_JSON";
		public const string _AUTH_ID = "AUTH_ID";
		public const string _USER_ID = "USER_ID";
		public const string _APP_ID = "APP_ID";
		public const string _GRANT_CODE = "GRANT_CODE";
		public const string _SCOPE_ID = "SCOPE_ID";
		public const string _CREATE_TIME = "CREATE_TIME";
		public const string _REMARKS = "REMARKS";
		public const string _EXPIRE_TIME = "EXPIRE_TIME";
		public const string _STATUS = "STATUS";
		public const string _DEVICE_ID = "DEVICE_ID";
		public const string _TableName = "TAUTH_CODE";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TAUTH_CODE");
			table.Columns.Add(_RIGHT_JSON,typeof(string)).DefaultValue=DBNull.Value;
			table.Columns.Add(_AUTH_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_USER_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_APP_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_GRANT_CODE,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_SCOPE_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_CREATE_TIME,typeof(DateTime)).DefaultValue=DateTime.Now;
			table.Columns.Add(_REMARKS,typeof(string)).DefaultValue=DBNull.Value;
			table.Columns.Add(_EXPIRE_TIME,typeof(DateTime)).DefaultValue=DBNull.Value;
			table.Columns.Add(_STATUS,typeof(int)).DefaultValue=0;
			table.Columns.Add(_DEVICE_ID,typeof(string)).DefaultValue=DBNull.Value;
			return table.NewRow();
		}
		#endregion
		
		#region 属性
		protected override string TableName
		{
			get{return _TableName;}
		}
		/// <summary>
		/// 授权权限Json字符串(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 4000Byte
		/// </para>
		/// </summary>
		public string Right_Json
		{
			get{ return DataRow[_RIGHT_JSON].ToString();}
			 set{setProperty(_RIGHT_JSON, value);}
		}
		/// <summary>
		/// 鉴权ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int Auth_Id
		{
			get{ return Convert.ToInt32(DataRow[_AUTH_ID]);}
			 set{setProperty(_AUTH_ID, value);}
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
		/// 应用ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int App_Id
		{
			get{ return Convert.ToInt32(DataRow[_APP_ID]);}
			 set{setProperty(_APP_ID, value);}
		}
		/// <summary>
		/// 授权码(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 50Byte
		/// </para>
		/// </summary>
		public string Grant_Code
		{
			get{ return DataRow[_GRANT_CODE].ToString();}
			 set{setProperty(_GRANT_CODE, value);}
		}
		/// <summary>
		/// 授权码作用域(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int Scope_Id
		{
			get{ return Convert.ToInt32(DataRow[_SCOPE_ID]);}
			 set{setProperty(_SCOPE_ID, value);}
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
		/// 备注信息(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 400Byte
		/// </para>
		/// </summary>
		public string Remarks
		{
			get{ return DataRow[_REMARKS].ToString();}
			 set{setProperty(_REMARKS, value);}
		}
		/// <summary>
		/// 授权码过期时间(必填)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 7Byte
		/// </para>
		/// </summary>
		public DateTime Expire_Time
		{
			get{ return Convert.ToDateTime(DataRow[_EXPIRE_TIME]);}
			 set{setProperty(_EXPIRE_TIME, value);}
		}
		/// <summary>
		/// 授权码状态[0：未使用，1：已使用](必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int Status
		{
			get{ return Convert.ToInt32(DataRow[_STATUS]);}
			 set{setProperty(_STATUS, value);}
		}
		/// <summary>
		/// 登录授权的设备ID(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 100Byte
		/// </para>
		/// </summary>
		public string Device_Id
		{
			get{ return DataRow[_DEVICE_ID].ToString();}
			 set{setProperty(_DEVICE_ID, value);}
		}
		#endregion
		
		#region 基本方法
		protected bool SelectByCondition(string condition)
		{
			string sql = "SELECT RIGHT_JSON,AUTH_ID,USER_ID,APP_ID,GRANT_CODE,SCOPE_ID,CREATE_TIME,REMARKS,EXPIRE_TIME,STATUS,DEVICE_ID FROM TAUTH_CODE WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TAUTH_CODE WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		public bool Delete(int auth_id)
		{
			string condition = " AUTH_ID=:AUTH_ID";
			AddParameter(_AUTH_ID,auth_id);
			return DeleteByCondition(condition);
		}
		public bool Delete()
		{
			string condition = " AUTH_ID=:AUTH_ID";
			AddParameter(_AUTH_ID,DataRow[_AUTH_ID]);
			return DeleteByCondition(condition);
		}
				
		public bool Insert()
		{		
			int id = this.Auth_Id = GetSequence("SELECT SEQ_TAUTH_CODE.nextval FROM DUAL");
			string sql = @"INSERT INTO TAUTH_CODE(RIGHT_JSON,AUTH_ID,USER_ID,APP_ID,GRANT_CODE,SCOPE_ID,REMARKS,EXPIRE_TIME,STATUS,DEVICE_ID)
			VALUES (:RIGHT_JSON,:AUTH_ID,:USER_ID,:APP_ID,:GRANT_CODE,:SCOPE_ID,:REMARKS,:EXPIRE_TIME,:STATUS,:DEVICE_ID)";
			AddParameter(_RIGHT_JSON,DataRow[_RIGHT_JSON]);
			AddParameter(_AUTH_ID,DataRow[_AUTH_ID]);
			AddParameter(_USER_ID,DataRow[_USER_ID]);
			AddParameter(_APP_ID,DataRow[_APP_ID]);
			AddParameter(_GRANT_CODE,DataRow[_GRANT_CODE]);
			AddParameter(_SCOPE_ID,DataRow[_SCOPE_ID]);
			AddParameter(_REMARKS,DataRow[_REMARKS]);
			AddParameter(_EXPIRE_TIME,DataRow[_EXPIRE_TIME]);
			AddParameter(_STATUS,DataRow[_STATUS]);
			AddParameter(_DEVICE_ID,DataRow[_DEVICE_ID]);
			return InsertBySql(sql);
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tauth_CodeCollection.Field,object> alterDic,Dictionary<Tauth_CodeCollection.Field,object> conditionDic)
		{
			if (alterDic.Count <= 0)
                return false;
            if (conditionDic.Count <= 0)
                return false;
            StringBuilder sql = new StringBuilder();
            sql.Append("update ").Append(_TableName).Append(" set ");
            foreach (Tauth_CodeCollection.Field key in alterDic.Keys)
            {
                object value = alterDic[key];
                string name = key.ToString();
                sql.Append(name).Append("=:").Append(name).Append(",");
                AddParameter(name, value);
            }
            sql.Remove(sql.Length - 1, 1);//移除最后一个逗号
            sql.Append(" where ");
            foreach (Tauth_CodeCollection.Field key in conditionDic.Keys)
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
			ChangePropertys.Remove(_AUTH_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TAUTH_CODE SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=:{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE AUTH_ID=:AUTH_ID");
			AddParameter(_AUTH_ID, DataRow[_AUTH_ID]);			
			if (!string.IsNullOrEmpty(condition))
            {
				sql.AppendLine(" AND " + condition);
			}
			bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
		}	
		public bool SelectByAppId_GrantCode(int app_id,string grant_code)
		{
			string condition = " APP_ID=:APP_ID AND GRANT_CODE=:GRANT_CODE";
			AddParameter(_APP_ID,app_id);
			AddParameter(_GRANT_CODE,grant_code);
			return SelectByCondition(condition);
		}
		public bool SelectByPk(int auth_id)
		{
			string condition = " AUTH_ID=:AUTH_ID";
			AddParameter(_AUTH_ID,auth_id);
			return SelectByCondition(condition);
		}
		#endregion
	}
	/// <summary>
	/// 授权码[集合对象]
	/// </summary>
	public partial class Tauth_CodeCollection : DataAccessCollectionBase
	{
		#region 构造和基本
		public Tauth_CodeCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tauth_Code().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tauth_Code(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tauth_Code._TableName;}
		}
		public Tauth_Code this[int index]
        {
            get { return new Tauth_Code(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Right_Json=0,
			Auth_Id=1,
			User_Id=2,
			App_Id=3,
			Grant_Code=4,
			Scope_Id=5,
			Create_Time=6,
			Remarks=7,
			Expire_Time=8,
			Status=9,
			Device_Id=10,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT RIGHT_JSON,AUTH_ID,USER_ID,APP_ID,GRANT_CODE,SCOPE_ID,CREATE_TIME,REMARKS,EXPIRE_TIME,STATUS,DEVICE_ID FROM TAUTH_CODE WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListAll()
		{
			string condition = " 1=1";
			return ListByCondition(condition);
		}
		#region Linq
		public Tauth_Code Find(Predicate<Tauth_Code> match)
        {
            foreach (Tauth_Code item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tauth_CodeCollection FindAll(Predicate<Tauth_Code> match)
        {
            Tauth_CodeCollection list = new Tauth_CodeCollection();
            foreach (Tauth_Code item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tauth_Code> match)
        {
            foreach (Tauth_Code item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tauth_Code> match)
        {
            BeginTransaction();
            foreach (Tauth_Code item in this)
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