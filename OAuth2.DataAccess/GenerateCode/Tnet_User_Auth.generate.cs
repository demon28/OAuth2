   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tnet_User_Auth.generate.cs
 * CreateTime : 2017-11-20 19:22:23
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
	/// 第三方授权登录会员
	/// </summary>
	public partial class Tnet_User_Auth : DataAccessBase
	{
		#region 构造和基本
		public Tnet_User_Auth():base()
		{}
		public Tnet_User_Auth(DataRow dataRow):base(dataRow)
		{}
		public const string _AUTH_ID = "AUTH_ID";
		public const string _USER_ID = "USER_ID";
		public const string _USER_NAME = "USER_NAME";
		public const string _THIRDPARTY = "THIRDPARTY";
		public const string _OPEN_ID = "OPEN_ID";
		public const string _AVATAR = "AVATAR";
		public const string _CREATETIME = "CREATETIME";
		public const string _STATUS = "STATUS";
		public const string _REMARKS = "REMARKS";
		public const string _TableName = "TNET_USER_AUTH";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TNET_USER_AUTH");
			table.Columns.Add(_AUTH_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_USER_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_USER_NAME,typeof(string)).DefaultValue=DBNull.Value;
			table.Columns.Add(_THIRDPARTY,typeof(int)).DefaultValue=0;
			table.Columns.Add(_OPEN_ID,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_AVATAR,typeof(string)).DefaultValue=DBNull.Value;
			table.Columns.Add(_CREATETIME,typeof(DateTime)).DefaultValue=DateTime.Now;
			table.Columns.Add(_STATUS,typeof(int)).DefaultValue=0;
			table.Columns.Add(_REMARKS,typeof(string)).DefaultValue=DBNull.Value;
			return table.NewRow();
		}
		#endregion
		
		#region 属性
		protected override string TableName
		{
			get{return _TableName;}
		}
		/// <summary>
		/// 授权登录会员ID(必填)
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
		/// 本地会员ID(必填)
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
		/// 昵称(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 40Byte
		/// </para>
		/// </summary>
		public string User_Name
		{
			get{ return DataRow[_USER_NAME].ToString();}
			 set{setProperty(_USER_NAME, value);}
		}
		/// <summary>
		/// 第三方[1：微信，2：QQ](必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int Thirdparty
		{
			get{ return Convert.ToInt32(DataRow[_THIRDPARTY]);}
			 set{setProperty(_THIRDPARTY, value);}
		}
		/// <summary>
		/// 第三方开放会员ID(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 200Byte
		/// </para>
		/// </summary>
		public string Open_Id
		{
			get{ return DataRow[_OPEN_ID].ToString();}
			 set{setProperty(_OPEN_ID, value);}
		}
		/// <summary>
		/// 头像地址(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 200Byte
		/// </para>
		/// </summary>
		public string Avatar
		{
			get{ return DataRow[_AVATAR].ToString();}
			 set{setProperty(_AVATAR, value);}
		}
		/// <summary>
		/// 创建时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 7Byte
		/// </para>
		/// </summary>
		public DateTime Createtime
		{
			get{ return Convert.ToDateTime(DataRow[_CREATETIME]);}
		}
		/// <summary>
		/// 状态(必填)
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
		#endregion
		
		#region 基本方法
		protected bool SelectByCondition(string condition)
		{
			string sql = "SELECT AUTH_ID,USER_ID,USER_NAME,THIRDPARTY,OPEN_ID,AVATAR,CREATETIME,STATUS,REMARKS FROM TNET_USER_AUTH WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TNET_USER_AUTH WHERE "+condition;
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
			int id = this.Auth_Id = GetSequence("SELECT SEQ_TNET_USER_AUTH.nextval FROM DUAL");
			string sql = @"INSERT INTO TNET_USER_AUTH(AUTH_ID,USER_ID,USER_NAME,THIRDPARTY,OPEN_ID,AVATAR,STATUS,REMARKS)
			VALUES (:AUTH_ID,:USER_ID,:USER_NAME,:THIRDPARTY,:OPEN_ID,:AVATAR,:STATUS,:REMARKS)";
			AddParameter(_AUTH_ID,DataRow[_AUTH_ID]);
			AddParameter(_USER_ID,DataRow[_USER_ID]);
			AddParameter(_USER_NAME,DataRow[_USER_NAME]);
			AddParameter(_THIRDPARTY,DataRow[_THIRDPARTY]);
			AddParameter(_OPEN_ID,DataRow[_OPEN_ID]);
			AddParameter(_AVATAR,DataRow[_AVATAR]);
			AddParameter(_STATUS,DataRow[_STATUS]);
			AddParameter(_REMARKS,DataRow[_REMARKS]);
			return InsertBySql(sql);
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tnet_User_AuthCollection.Field,object> alterDic,Dictionary<Tnet_User_AuthCollection.Field,object> conditionDic)
		{
			if (alterDic.Count <= 0)
                return false;
            if (conditionDic.Count <= 0)
                return false;
            StringBuilder sql = new StringBuilder();
            sql.Append("update ").Append(_TableName).Append(" set ");
            foreach (Tnet_User_AuthCollection.Field key in alterDic.Keys)
            {
                object value = alterDic[key];
                string name = key.ToString();
                sql.Append(name).Append("=:").Append(name).Append(",");
                AddParameter(name, value);
            }
            sql.Remove(sql.Length - 1, 1);//移除最后一个逗号
            sql.Append(" where ");
            foreach (Tnet_User_AuthCollection.Field key in conditionDic.Keys)
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
            sql.AppendLine("UPDATE TNET_USER_AUTH SET");
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
		public bool SelectByUserId_Thirdparty(int user_id,int thirdparty)
		{
			string condition = " USER_ID=:USER_ID AND THIRDPARTY=:THIRDPARTY";
			AddParameter(_USER_ID,user_id);
			AddParameter(_THIRDPARTY,thirdparty);
			return SelectByCondition(condition);
		}
		public bool SelectByThirdparty_OpenId(int thirdparty,string open_id)
		{
			string condition = " THIRDPARTY=:THIRDPARTY AND OPEN_ID=:OPEN_ID";
			AddParameter(_THIRDPARTY,thirdparty);
			AddParameter(_OPEN_ID,open_id);
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
	/// 第三方授权登录会员[集合对象]
	/// </summary>
	public partial class Tnet_User_AuthCollection : DataAccessCollectionBase
	{
		#region 构造和基本
		public Tnet_User_AuthCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tnet_User_Auth().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tnet_User_Auth(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tnet_User_Auth._TableName;}
		}
		public Tnet_User_Auth this[int index]
        {
            get { return new Tnet_User_Auth(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Auth_Id=0,
			User_Id=1,
			User_Name=2,
			Thirdparty=3,
			Open_Id=4,
			Avatar=5,
			Createtime=6,
			Status=7,
			Remarks=8,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT AUTH_ID,USER_ID,USER_NAME,THIRDPARTY,OPEN_ID,AVATAR,CREATETIME,STATUS,REMARKS FROM TNET_USER_AUTH WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListAll()
		{
			string condition = " 1=1";
			return ListByCondition(condition);
		}
		#region Linq
		public Tnet_User_Auth Find(Predicate<Tnet_User_Auth> match)
        {
            foreach (Tnet_User_Auth item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tnet_User_AuthCollection FindAll(Predicate<Tnet_User_Auth> match)
        {
            Tnet_User_AuthCollection list = new Tnet_User_AuthCollection();
            foreach (Tnet_User_Auth item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tnet_User_Auth> match)
        {
            foreach (Tnet_User_Auth item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tnet_User_Auth> match)
        {
            BeginTransaction();
            foreach (Tnet_User_Auth item in this)
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