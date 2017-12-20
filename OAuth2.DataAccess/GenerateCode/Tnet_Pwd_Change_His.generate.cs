   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tnet_Pwd_Change_His.generate.cs
 * CreateTime : 2017-08-16 15:36:09
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
	/// 
	/// </summary>
	public partial class Tnet_Pwd_Change_His : DataAccessBase
	{
		#region 构造和基本
		public Tnet_Pwd_Change_His():base()
		{}
		public Tnet_Pwd_Change_His(DataRow dataRow):base(dataRow)
		{}
		public const string _HISID = "HISID";
		public const string _USER_ID = "USER_ID";
		public const string _OLD_PWD = "OLD_PWD";
		public const string _NEW_PWD = "NEW_PWD";
		public const string _PWD_TYPE = "PWD_TYPE";
		public const string _ALTER_PLACE = "ALTER_PLACE";
		public const string _CREATETIME = "CREATETIME";
		public const string _REMARKS = "REMARKS";
		public const string _ALTER_SOURCE = "ALTER_SOURCE";
		public const string _TableName = "TNET_PWD_CHANGE_HIS";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TNET_PWD_CHANGE_HIS");
			table.Columns.Add(_HISID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_USER_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_OLD_PWD,typeof(string)).DefaultValue=DBNull.Value;
			table.Columns.Add(_NEW_PWD,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_PWD_TYPE,typeof(int)).DefaultValue=0;
			table.Columns.Add(_ALTER_PLACE,typeof(string)).DefaultValue=DBNull.Value;
			table.Columns.Add(_CREATETIME,typeof(DateTime)).DefaultValue=DateTime.Now;
			table.Columns.Add(_REMARKS,typeof(string)).DefaultValue=DBNull.Value;
			table.Columns.Add(_ALTER_SOURCE,typeof(string)).DefaultValue=DBNull.Value;
			return table.NewRow();
		}
		#endregion
		
		#region 属性
		protected override string TableName
		{
			get{return _TableName;}
		}
		/// <summary>
		/// 记录ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int Hisid
		{
			get{ return Convert.ToInt32(DataRow[_HISID]);}
			 set{setProperty(_HISID, value);}
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
		/// 旧密码（密文）(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 200Byte
		/// </para>
		/// </summary>
		public string Old_Pwd
		{
			get{ return DataRow[_OLD_PWD].ToString();}
			 set{setProperty(_OLD_PWD, value);}
		}
		/// <summary>
		/// 新密码（密文）(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 200Byte
		/// </para>
		/// </summary>
		public string New_Pwd
		{
			get{ return DataRow[_NEW_PWD].ToString();}
			 set{setProperty(_NEW_PWD, value);}
		}
		/// <summary>
		/// 密码类型，0：登录密码，1：支付密码(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int Pwd_Type
		{
			get{ return Convert.ToInt32(DataRow[_PWD_TYPE]);}
			 set{setProperty(_PWD_TYPE, value);}
		}
		/// <summary>
		/// 修改场合(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 100Byte
		/// </para>
		/// </summary>
		public string Alter_Place
		{
			get{ return DataRow[_ALTER_PLACE].ToString();}
			 set{setProperty(_ALTER_PLACE, value);}
		}
		/// <summary>
		/// 修改时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 7Byte
		/// </para>
		/// </summary>
		public DateTime Createtime
		{
			get{ return Convert.ToDateTime(DataRow[_CREATETIME]);}
		}
		/// <summary>
		/// 备注信息(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 280Byte
		/// </para>
		/// </summary>
		public string Remarks
		{
			get{ return DataRow[_REMARKS].ToString();}
			 set{setProperty(_REMARKS, value);}
		}
		/// <summary>
		/// 修改来源[Android,iOS,PC,Wechat,...etc..](可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 10Byte
		/// </para>
		/// </summary>
		public string Alter_Source
		{
			get{ return DataRow[_ALTER_SOURCE].ToString();}
			 set{setProperty(_ALTER_SOURCE, value);}
		}
		#endregion
		
		#region 基本方法
		protected bool SelectByCondition(string condition)
		{
			string sql = "SELECT HISID,USER_ID,OLD_PWD,NEW_PWD,PWD_TYPE,ALTER_PLACE,CREATETIME,REMARKS,ALTER_SOURCE FROM TNET_PWD_CHANGE_HIS WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TNET_PWD_CHANGE_HIS WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		public bool Delete(int hisid)
		{
			string condition = " HISID=:HISID";
			AddParameter(_HISID,hisid);
			return DeleteByCondition(condition);
		}
		public bool Delete()
		{
			string condition = " HISID=:HISID";
			AddParameter(_HISID,DataRow[_HISID]);
			return DeleteByCondition(condition);
		}
				
		public bool Insert()
		{		
			int id = this.Hisid = GetSequence("SELECT SEQ_TNET_PWD_CHANGE_HIS.nextval FROM DUAL");
			string sql = @"INSERT INTO TNET_PWD_CHANGE_HIS(HISID,USER_ID,OLD_PWD,NEW_PWD,PWD_TYPE,ALTER_PLACE,REMARKS,ALTER_SOURCE)
			VALUES (:HISID,:USER_ID,:OLD_PWD,:NEW_PWD,:PWD_TYPE,:ALTER_PLACE,:REMARKS,:ALTER_SOURCE)";
			AddParameter(_HISID,DataRow[_HISID]);
			AddParameter(_USER_ID,DataRow[_USER_ID]);
			AddParameter(_OLD_PWD,DataRow[_OLD_PWD]);
			AddParameter(_NEW_PWD,DataRow[_NEW_PWD]);
			AddParameter(_PWD_TYPE,DataRow[_PWD_TYPE]);
			AddParameter(_ALTER_PLACE,DataRow[_ALTER_PLACE]);
			AddParameter(_REMARKS,DataRow[_REMARKS]);
			AddParameter(_ALTER_SOURCE,DataRow[_ALTER_SOURCE]);
			return InsertBySql(sql);
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tnet_Pwd_Change_HisCollection.Field,object> alterDic,Dictionary<Tnet_Pwd_Change_HisCollection.Field,object> conditionDic)
		{
			if (alterDic.Count <= 0)
                return false;
            if (conditionDic.Count <= 0)
                return false;
            StringBuilder sql = new StringBuilder();
            sql.Append("update ").Append(_TableName).Append(" set ");
            foreach (Tnet_Pwd_Change_HisCollection.Field key in alterDic.Keys)
            {
                object value = alterDic[key];
                string name = key.ToString();
                sql.Append(name).Append("=:").Append(name).Append(",");
                AddParameter(name, value);
            }
            sql.Remove(sql.Length - 1, 1);//移除最后一个逗号
            sql.Append(" where ");
            foreach (Tnet_Pwd_Change_HisCollection.Field key in conditionDic.Keys)
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
			ChangePropertys.Remove(_HISID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TNET_PWD_CHANGE_HIS SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=:{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE HISID=:HISID");
			AddParameter(_HISID, DataRow[_HISID]);			
			if (!string.IsNullOrEmpty(condition))
            {
				sql.AppendLine(" AND " + condition);
			}
			bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
		}	
		public bool SelectByPk(int hisid)
		{
			string condition = " HISID=:HISID";
			AddParameter(_HISID,hisid);
			return SelectByCondition(condition);
		}
		#endregion
	}
	
	public partial class Tnet_Pwd_Change_HisCollection : DataAccessCollectionBase
	{
		#region 构造和基本
		public Tnet_Pwd_Change_HisCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tnet_Pwd_Change_His().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tnet_Pwd_Change_His(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tnet_Pwd_Change_His._TableName;}
		}
		public Tnet_Pwd_Change_His this[int index]
        {
            get { return new Tnet_Pwd_Change_His(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Hisid=0,
			User_Id=1,
			Old_Pwd=2,
			New_Pwd=3,
			Pwd_Type=4,
			Alter_Place=5,
			Createtime=6,
			Remarks=7,
			Alter_Source=8,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT HISID,USER_ID,OLD_PWD,NEW_PWD,PWD_TYPE,ALTER_PLACE,CREATETIME,REMARKS,ALTER_SOURCE FROM TNET_PWD_CHANGE_HIS WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListAll()
		{
			string condition = " 1=1";
			return ListByCondition(condition);
		}
		#region Linq
		public Tnet_Pwd_Change_His Find(Predicate<Tnet_Pwd_Change_His> match)
        {
            foreach (Tnet_Pwd_Change_His item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tnet_Pwd_Change_HisCollection FindAll(Predicate<Tnet_Pwd_Change_His> match)
        {
            Tnet_Pwd_Change_HisCollection list = new Tnet_Pwd_Change_HisCollection();
            foreach (Tnet_Pwd_Change_His item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tnet_Pwd_Change_His> match)
        {
            foreach (Tnet_Pwd_Change_His item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tnet_Pwd_Change_His> match)
        {
            BeginTransaction();
            foreach (Tnet_Pwd_Change_His item in this)
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