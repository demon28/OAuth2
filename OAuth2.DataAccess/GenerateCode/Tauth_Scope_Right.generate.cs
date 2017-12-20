   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tauth_Scope_Right.generate.cs
 * CreateTime : 2017-11-23 09:31:15
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
	/// 授权作用域的权限
	/// </summary>
	public partial class Tauth_Scope_Right : DataAccessBase
	{
		#region 构造和基本
		public Tauth_Scope_Right():base()
		{}
		public Tauth_Scope_Right(DataRow dataRow):base(dataRow)
		{}
		public const string _RIGHT_ID = "RIGHT_ID";
		public const string _SCOPE_ID = "SCOPE_ID";
		public const string _REF_ID = "REF_ID";
		public const string _REF_TYPE = "REF_TYPE";
		public const string _CREATE_TIME = "CREATE_TIME";
		public const string _REMARKS = "REMARKS";
		public const string _TableName = "TAUTH_SCOPE_RIGHT";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TAUTH_SCOPE_RIGHT");
			table.Columns.Add(_RIGHT_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_SCOPE_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_REF_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_REF_TYPE,typeof(int)).DefaultValue=0;
			table.Columns.Add(_CREATE_TIME,typeof(DateTime)).DefaultValue=DateTime.Now;
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
		/// 主键(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int Right_Id
		{
			get{ return Convert.ToInt32(DataRow[_RIGHT_ID]);}
			 set{setProperty(_RIGHT_ID, value);}
		}
		/// <summary>
		/// 作用域ID(必填)
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
		/// 关联ID（REF_TYPE=1关联GROUP，REF_TYPE=2关联API）(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int Ref_Id
		{
			get{ return Convert.ToInt32(DataRow[_REF_ID]);}
			 set{setProperty(_REF_ID, value);}
		}
		/// <summary>
		/// 关联类型[0：关联TAUTH_API_GROUP，1：关联TAUTH_API_INFO](必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int Ref_Type
		{
			get{ return Convert.ToInt32(DataRow[_REF_TYPE]);}
			 set{setProperty(_REF_TYPE, value);}
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
		#endregion
		
		#region 基本方法
		protected bool SelectByCondition(string condition)
		{
			string sql = "SELECT RIGHT_ID,SCOPE_ID,REF_ID,REF_TYPE,CREATE_TIME,REMARKS FROM TAUTH_SCOPE_RIGHT WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TAUTH_SCOPE_RIGHT WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		public bool Delete(int right_id)
		{
			string condition = " RIGHT_ID=:RIGHT_ID";
			AddParameter(_RIGHT_ID,right_id);
			return DeleteByCondition(condition);
		}
		public bool Delete()
		{
			string condition = " RIGHT_ID=:RIGHT_ID";
			AddParameter(_RIGHT_ID,DataRow[_RIGHT_ID]);
			return DeleteByCondition(condition);
		}
				
		public bool Insert()
		{		
			int id = this.Right_Id = GetSequence("SELECT SEQ_TAUTH_SCOPE_RIGHT.nextval FROM DUAL");
			string sql = @"INSERT INTO TAUTH_SCOPE_RIGHT(RIGHT_ID,SCOPE_ID,REF_ID,REF_TYPE,REMARKS)
			VALUES (:RIGHT_ID,:SCOPE_ID,:REF_ID,:REF_TYPE,:REMARKS)";
			AddParameter(_RIGHT_ID,DataRow[_RIGHT_ID]);
			AddParameter(_SCOPE_ID,DataRow[_SCOPE_ID]);
			AddParameter(_REF_ID,DataRow[_REF_ID]);
			AddParameter(_REF_TYPE,DataRow[_REF_TYPE]);
			AddParameter(_REMARKS,DataRow[_REMARKS]);
			return InsertBySql(sql);
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tauth_Scope_RightCollection.Field,object> alterDic,Dictionary<Tauth_Scope_RightCollection.Field,object> conditionDic)
		{
			if (alterDic.Count <= 0)
                return false;
            if (conditionDic.Count <= 0)
                return false;
            StringBuilder sql = new StringBuilder();
            sql.Append("update ").Append(_TableName).Append(" set ");
            foreach (Tauth_Scope_RightCollection.Field key in alterDic.Keys)
            {
                object value = alterDic[key];
                string name = key.ToString();
                sql.Append(name).Append("=:").Append(name).Append(",");
                AddParameter(name, value);
            }
            sql.Remove(sql.Length - 1, 1);//移除最后一个逗号
            sql.Append(" where ");
            foreach (Tauth_Scope_RightCollection.Field key in conditionDic.Keys)
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
			ChangePropertys.Remove(_RIGHT_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TAUTH_SCOPE_RIGHT SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=:{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE RIGHT_ID=:RIGHT_ID");
			AddParameter(_RIGHT_ID, DataRow[_RIGHT_ID]);			
			if (!string.IsNullOrEmpty(condition))
            {
				sql.AppendLine(" AND " + condition);
			}
			bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
		}	
		public bool SelectByPk(int right_id)
		{
			string condition = " RIGHT_ID=:RIGHT_ID";
			AddParameter(_RIGHT_ID,right_id);
			return SelectByCondition(condition);
		}
		#endregion
	}
	/// <summary>
	/// 授权作用域的权限[集合对象]
	/// </summary>
	public partial class Tauth_Scope_RightCollection : DataAccessCollectionBase
	{
		#region 构造和基本
		public Tauth_Scope_RightCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tauth_Scope_Right().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tauth_Scope_Right(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tauth_Scope_Right._TableName;}
		}
		public Tauth_Scope_Right this[int index]
        {
            get { return new Tauth_Scope_Right(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Right_Id=0,
			Scope_Id=1,
			Ref_Id=2,
			Ref_Type=3,
			Create_Time=4,
			Remarks=5,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT RIGHT_ID,SCOPE_ID,REF_ID,REF_TYPE,CREATE_TIME,REMARKS FROM TAUTH_SCOPE_RIGHT WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListAll()
		{
			string condition = " 1=1";
			return ListByCondition(condition);
		}
		#region Linq
		public Tauth_Scope_Right Find(Predicate<Tauth_Scope_Right> match)
        {
            foreach (Tauth_Scope_Right item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tauth_Scope_RightCollection FindAll(Predicate<Tauth_Scope_Right> match)
        {
            Tauth_Scope_RightCollection list = new Tauth_Scope_RightCollection();
            foreach (Tauth_Scope_Right item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tauth_Scope_Right> match)
        {
            foreach (Tauth_Scope_Right item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tauth_Scope_Right> match)
        {
            BeginTransaction();
            foreach (Tauth_Scope_Right item in this)
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