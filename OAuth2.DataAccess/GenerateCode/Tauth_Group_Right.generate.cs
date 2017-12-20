   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tauth_Group_Right.generate.cs
 * CreateTime : 2017-11-23 11:38:41
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
	/// API分组权限
	/// </summary>
	public partial class Tauth_Group_Right : DataAccessBase
	{
		#region 构造和基本
		public Tauth_Group_Right():base()
		{}
		public Tauth_Group_Right(DataRow dataRow):base(dataRow)
		{}
		public const string _RIGHT_ID = "RIGHT_ID";
		public const string _GROUP_ID = "GROUP_ID";
		public const string _API_ID = "API_ID";
		public const string _CREATE_TIME = "CREATE_TIME";
		public const string _REMARKS = "REMARKS";
		public const string _TableName = "TAUTH_GROUP_RIGHT";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TAUTH_GROUP_RIGHT");
			table.Columns.Add(_RIGHT_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_GROUP_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_API_ID,typeof(int)).DefaultValue=0;
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
		/// 分组ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int Group_Id
		{
			get{ return Convert.ToInt32(DataRow[_GROUP_ID]);}
			 set{setProperty(_GROUP_ID, value);}
		}
		/// <summary>
		/// 接口ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int Api_Id
		{
			get{ return Convert.ToInt32(DataRow[_API_ID]);}
			 set{setProperty(_API_ID, value);}
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
			string sql = "SELECT RIGHT_ID,GROUP_ID,API_ID,CREATE_TIME,REMARKS FROM TAUTH_GROUP_RIGHT WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TAUTH_GROUP_RIGHT WHERE "+condition;
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
			int id = this.Right_Id = GetSequence("SELECT SEQ_TAUTH_GROUP_RIGHT.nextval FROM DUAL");
			string sql = @"INSERT INTO TAUTH_GROUP_RIGHT(RIGHT_ID,GROUP_ID,API_ID,REMARKS)
			VALUES (:RIGHT_ID,:GROUP_ID,:API_ID,:REMARKS)";
			AddParameter(_RIGHT_ID,DataRow[_RIGHT_ID]);
			AddParameter(_GROUP_ID,DataRow[_GROUP_ID]);
			AddParameter(_API_ID,DataRow[_API_ID]);
			AddParameter(_REMARKS,DataRow[_REMARKS]);
			return InsertBySql(sql);
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tauth_Group_RightCollection.Field,object> alterDic,Dictionary<Tauth_Group_RightCollection.Field,object> conditionDic)
		{
			if (alterDic.Count <= 0)
                return false;
            if (conditionDic.Count <= 0)
                return false;
            StringBuilder sql = new StringBuilder();
            sql.Append("update ").Append(_TableName).Append(" set ");
            foreach (Tauth_Group_RightCollection.Field key in alterDic.Keys)
            {
                object value = alterDic[key];
                string name = key.ToString();
                sql.Append(name).Append("=:").Append(name).Append(",");
                AddParameter(name, value);
            }
            sql.Remove(sql.Length - 1, 1);//移除最后一个逗号
            sql.Append(" where ");
            foreach (Tauth_Group_RightCollection.Field key in conditionDic.Keys)
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
            sql.AppendLine("UPDATE TAUTH_GROUP_RIGHT SET");
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
	/// API分组权限[集合对象]
	/// </summary>
	public partial class Tauth_Group_RightCollection : DataAccessCollectionBase
	{
		#region 构造和基本
		public Tauth_Group_RightCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tauth_Group_Right().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tauth_Group_Right(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tauth_Group_Right._TableName;}
		}
		public Tauth_Group_Right this[int index]
        {
            get { return new Tauth_Group_Right(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Right_Id=0,
			Group_Id=1,
			Api_Id=2,
			Create_Time=3,
			Remarks=4,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT RIGHT_ID,GROUP_ID,API_ID,CREATE_TIME,REMARKS FROM TAUTH_GROUP_RIGHT WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListByGroup_Id(int group_id)
		{
			string condition = "GROUP_ID=:GROUP_ID ORDER BY RIGHT_ID DESC";
			AddParameter(Tauth_Group_Right._GROUP_ID,group_id);
			return ListByCondition(condition);		
		}
		public bool ListAll()
		{
			string condition = " 1=1";
			return ListByCondition(condition);
		}
		#region Linq
		public Tauth_Group_Right Find(Predicate<Tauth_Group_Right> match)
        {
            foreach (Tauth_Group_Right item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tauth_Group_RightCollection FindAll(Predicate<Tauth_Group_Right> match)
        {
            Tauth_Group_RightCollection list = new Tauth_Group_RightCollection();
            foreach (Tauth_Group_Right item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tauth_Group_Right> match)
        {
            foreach (Tauth_Group_Right item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tauth_Group_Right> match)
        {
            BeginTransaction();
            foreach (Tauth_Group_Right item in this)
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