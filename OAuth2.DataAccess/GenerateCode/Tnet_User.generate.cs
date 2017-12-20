/***************************************************
*
* Data Access Layer Of Winner Framework
* FileName : Tnet_User.generate.cs
* CreateTime : 2017-09-11 15:07:58
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
using Winner.Framework.Utils;

namespace OAuth2.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Tnet_User : DataAccessBase
    {
        #region 构造和基本
        public Tnet_User() : base()
        { }
        public Tnet_User(DataRow dataRow) : base(dataRow)
        { }
        public const string _USER_ID = "USER_ID";
        public const string _USER_CODE = "USER_CODE";
        public const string _USER_NICKNAME = "USER_NICKNAME";
        public const string _USER_NAME = "USER_NAME";
        public const string _FATHER_ID = "FATHER_ID";
        public const string _USER_STATUS = "USER_STATUS";
        public const string _USER_LEVEL = "USER_LEVEL";
        public const string _E_MAIL = "E_MAIL";
        public const string _MOBILE_NO = "MOBILE_NO";
        public const string _AUTH_STATUS = "AUTH_STATUS";
        public const string _AUTH_TIME = "AUTH_TIME";
        public const string _LOGIN_PASSWORD = "LOGIN_PASSWORD";
        public const string _PAYMENT_PASSWORD = "PAYMENT_PASSWORD";
        public const string _PHOTO_URL = "PHOTO_URL";
        public const string _DATA_SOURCE = "DATA_SOURCE";
        public const string _REMARKS = "REMARKS";
        public const string _CREATE_TIME = "CREATE_TIME";
        public const string _TableName = "TNET_USER";
        protected override DataRow BuildRow()
        {
            DataTable table = new DataTable("TNET_USER");
            table.Columns.Add(_USER_ID, typeof(int)).DefaultValue = 0;
            table.Columns.Add(_USER_CODE, typeof(string)).DefaultValue = string.Empty;
            table.Columns.Add(_USER_NICKNAME, typeof(string)).DefaultValue = DBNull.Value;
            table.Columns.Add(_USER_NAME, typeof(string)).DefaultValue = string.Empty;
            table.Columns.Add(_FATHER_ID, typeof(int)).DefaultValue = DBNull.Value;
            table.Columns.Add(_USER_STATUS, typeof(int)).DefaultValue = 0;
            table.Columns.Add(_USER_LEVEL, typeof(int)).DefaultValue = 0;
            table.Columns.Add(_E_MAIL, typeof(string)).DefaultValue = DBNull.Value;
            table.Columns.Add(_MOBILE_NO, typeof(string)).DefaultValue = string.Empty;
            table.Columns.Add(_AUTH_STATUS, typeof(int)).DefaultValue = 0;
            table.Columns.Add(_AUTH_TIME, typeof(DateTime)).DefaultValue = DBNull.Value;
            table.Columns.Add(_LOGIN_PASSWORD, typeof(string)).DefaultValue = DBNull.Value;
            table.Columns.Add(_PAYMENT_PASSWORD, typeof(string)).DefaultValue = DBNull.Value;
            table.Columns.Add(_PHOTO_URL, typeof(string)).DefaultValue = DBNull.Value;
            table.Columns.Add(_DATA_SOURCE, typeof(int)).DefaultValue = 1;
            table.Columns.Add(_REMARKS, typeof(string)).DefaultValue = DBNull.Value;
            table.Columns.Add(_CREATE_TIME, typeof(DateTime)).DefaultValue = DateTime.Now;
            return table.NewRow();
        }
        #endregion

        #region 属性
        protected override string TableName
        {
            get { return _TableName; }
        }
        /// <summary>
        /// 用户编号(必填)
        /// <para>
        /// defaultValue: 0;   Length: 22Byte
        /// </para>
        /// </summary>
        public int User_Id
        {
            get { return Convert.ToInt32(DataRow[_USER_ID]); }
            set { setProperty(_USER_ID, value); }
        }
        /// <summary>
        /// 用户账号(必填)
        /// <para>
        /// defaultValue: string.Empty;   Length: 40Byte
        /// </para>
        /// </summary>
        public string User_Code
        {
            get { return DataRow[_USER_CODE].ToString(); }
            set { setProperty(_USER_CODE, value); }
        }
        /// <summary>
        /// 用户昵称(可空)
        /// <para>
        /// defaultValue: DBNull.Value;   Length: 100Byte
        /// </para>
        /// </summary>
        public string User_Nickname
        {
            get { return DataRow[_USER_NICKNAME].ToString(); }
            set { setProperty(_USER_NICKNAME, value); }
        }
        /// <summary>
        /// 用户姓名(必填)
        /// <para>
        /// defaultValue: string.Empty;   Length: 100Byte
        /// </para>
        /// </summary>
        public string User_Name
        {
            get { return DataRow[_USER_NAME].ToString(); }
            set { setProperty(_USER_NAME, value); }
        }
        /// <summary>
        /// 推荐人用户编号(可空)
        /// <para>
        /// defaultValue: DBNull.Value;   Length: 22Byte
        /// </para>
        /// </summary>
        public int? Father_Id
        {
            get { return Helper.ToInt32(DataRow[_FATHER_ID]); }
            set { setProperty(_FATHER_ID, value); }
        }
        /// <summary>
        /// 用户状态$UserStatus$,未激活=0,已激活=1,已注销=2,已封锁=3(必填)
        /// <para>
        /// defaultValue: 0;   Length: 22Byte
        /// </para>
        /// </summary>
        public int User_Status
        {
            get { return Convert.ToInt32(DataRow[_USER_STATUS]); }
            set { setProperty(_USER_STATUS, value); }
        }
        /// <summary>
        /// 用户级别(必填)
        /// <para>
        /// defaultValue: 0;   Length: 22Byte
        /// </para>
        /// </summary>
        public int User_Level
        {
            get { return Convert.ToInt32(DataRow[_USER_LEVEL]); }
            set { setProperty(_USER_LEVEL, value); }
        }
        /// <summary>
        /// E-Mail(可空)
        /// <para>
        /// defaultValue: DBNull.Value;   Length: 40Byte
        /// </para>
        /// </summary>
        public string E_Mail
        {
            get { return DataRow[_E_MAIL].ToString(); }
            set { setProperty(_E_MAIL, value); }
        }
        /// <summary>
        /// 手机号码(必填)
        /// <para>
        /// defaultValue: string.Empty;   Length: 20Byte
        /// </para>
        /// </summary>
        public string Mobile_No
        {
            get { return DataRow[_MOBILE_NO].ToString(); }
            set { setProperty(_MOBILE_NO, value); }
        }
        /// <summary>
        /// 实名认证状态$AuthStatus${未实名=0,审核中=1,已认证=2，认证失败=4}(必填)
        /// <para>
        /// defaultValue: 0;   Length: 22Byte
        /// </para>
        /// </summary>
        public int Auth_Status
        {
            get { return Convert.ToInt32(DataRow[_AUTH_STATUS]); }
            set { setProperty(_AUTH_STATUS, value); }
        }
        /// <summary>
        /// 实名验证时间(可空)
        /// <para>
        /// defaultValue: DBNull.Value;   Length: 7Byte
        /// </para>
        /// </summary>
        public DateTime? Auth_Time
        {
            get { return Helper.ToDateTime(DataRow[_AUTH_TIME]); }
            set { setProperty(_AUTH_TIME, value); }
        }
        /// <summary>
        /// 用户登陆密码(可空)
        /// <para>
        /// defaultValue: DBNull.Value;   Length: 200Byte
        /// </para>
        /// </summary>
        public string Login_Password
        {
            get { return DataRow[_LOGIN_PASSWORD].ToString(); }
            set { setProperty(_LOGIN_PASSWORD, value); }
        }
        /// <summary>
        /// 用户消费密码(可空)
        /// <para>
        /// defaultValue: DBNull.Value;   Length: 200Byte
        /// </para>
        /// </summary>
        public string Payment_Password
        {
            get { return DataRow[_PAYMENT_PASSWORD].ToString(); }
            set { setProperty(_PAYMENT_PASSWORD, value); }
        }
        /// <summary>
        /// 用户头像(可空)
        /// <para>
        /// defaultValue: DBNull.Value;   Length: 200Byte
        /// </para>
        /// </summary>
        public string Photo_Url
        {
            get { return DataRow[_PHOTO_URL].ToString(); }
            set { setProperty(_PHOTO_URL, value); }
        }
        /// <summary>
        /// 数据来源(必填)
        /// <para>
        /// defaultValue: 1;   Length: 22Byte
        /// </para>
        /// </summary>
        public int Data_Source
        {
            get { return Convert.ToInt32(DataRow[_DATA_SOURCE]); }
            set { setProperty(_DATA_SOURCE, value); }
        }
        /// <summary>
        /// 备注信息(可空)
        /// <para>
        /// defaultValue: DBNull.Value;   Length: 400Byte
        /// </para>
        /// </summary>
        public string Remarks
        {
            get { return DataRow[_REMARKS].ToString(); }
            set { setProperty(_REMARKS, value); }
        }
        /// <summary>
        /// 录入时间(必填)
        /// <para>
        /// defaultValue: DateTime.Now;   Length: 7Byte
        /// </para>
        /// </summary>
        public DateTime Create_Time
        {
            get { return Convert.ToDateTime(DataRow[_CREATE_TIME]); }
            set { setProperty(_CREATE_TIME, value); }
        }
        #endregion

        #region 基本方法
        protected bool SelectByCondition(string condition)
        {
            string sql = "SELECT USER_ID,USER_CODE,USER_NICKNAME,USER_NAME,FATHER_ID,USER_STATUS,USER_LEVEL,E_MAIL,MOBILE_NO,AUTH_STATUS,AUTH_TIME,LOGIN_PASSWORD,PAYMENT_PASSWORD,PHOTO_URL,DATA_SOURCE,REMARKS,CREATE_TIME FROM TNET_USER WHERE " + condition;
            return base.SelectBySql(sql);
        }
        protected bool DeleteByCondition(string condition)
        {
            string sql = "DELETE FROM TNET_USER WHERE " + condition;
            return base.DeleteBySql(sql);
        }

        public bool Delete(int user_id)
        {
            string condition = " USER_ID=:USER_ID";
            AddParameter(_USER_ID, user_id);
            return DeleteByCondition(condition);
        }
        public bool Delete()
        {
            string condition = " USER_ID=:USER_ID";
            AddParameter(_USER_ID, DataRow[_USER_ID]);
            return DeleteByCondition(condition);
        }

        public bool Insert(int userId)
        {
            //int id = this.User_Id = GetSequence("SELECT SEQ_TNET_USER.nextval FROM DUAL");
            this.User_Id = userId;
            string sql = @"INSERT INTO TNET_USER(USER_ID,USER_CODE,USER_NICKNAME,USER_NAME,FATHER_ID,USER_STATUS,USER_LEVEL,E_MAIL,MOBILE_NO,AUTH_STATUS,AUTH_TIME,LOGIN_PASSWORD,PAYMENT_PASSWORD,PHOTO_URL,DATA_SOURCE,REMARKS)
			VALUES (:USER_ID,:USER_CODE,:USER_NICKNAME,:USER_NAME,:FATHER_ID,:USER_STATUS,:USER_LEVEL,:E_MAIL,:MOBILE_NO,:AUTH_STATUS,:AUTH_TIME,:LOGIN_PASSWORD,:PAYMENT_PASSWORD,:PHOTO_URL,:DATA_SOURCE,:REMARKS)";
            AddParameter(_USER_ID, DataRow[_USER_ID]);
            AddParameter(_USER_CODE, DataRow[_USER_CODE]);
            AddParameter(_USER_NICKNAME, DataRow[_USER_NICKNAME]);
            AddParameter(_USER_NAME, DataRow[_USER_NAME]);
            AddParameter(_FATHER_ID, DataRow[_FATHER_ID]);
            AddParameter(_USER_STATUS, DataRow[_USER_STATUS]);
            AddParameter(_USER_LEVEL, DataRow[_USER_LEVEL]);
            AddParameter(_E_MAIL, DataRow[_E_MAIL]);
            AddParameter(_MOBILE_NO, DataRow[_MOBILE_NO]);
            AddParameter(_AUTH_STATUS, DataRow[_AUTH_STATUS]);
            AddParameter(_AUTH_TIME, DataRow[_AUTH_TIME]);
            AddParameter(_LOGIN_PASSWORD, DataRow[_LOGIN_PASSWORD]);
            AddParameter(_PAYMENT_PASSWORD, DataRow[_PAYMENT_PASSWORD]);
            AddParameter(_PHOTO_URL, DataRow[_PHOTO_URL]);
            AddParameter(_DATA_SOURCE, DataRow[_DATA_SOURCE]);
            AddParameter(_REMARKS, DataRow[_REMARKS]);
            return InsertBySql(sql);
        }

        public bool Update()
        {
            return UpdateByCondition(string.Empty);
        }
        public bool Update(Dictionary<Tnet_UserCollection.Field, object> alterDic, Dictionary<Tnet_UserCollection.Field, object> conditionDic)
        {
            if (alterDic.Count <= 0)
                return false;
            if (conditionDic.Count <= 0)
                return false;
            StringBuilder sql = new StringBuilder();
            sql.Append("update ").Append(_TableName).Append(" set ");
            foreach (Tnet_UserCollection.Field key in alterDic.Keys)
            {
                object value = alterDic[key];
                string name = key.ToString();
                sql.Append(name).Append("=:").Append(name).Append(",");
                AddParameter(name, value);
            }
            sql.Remove(sql.Length - 1, 1);//移除最后一个逗号
            sql.Append(" where ");
            foreach (Tnet_UserCollection.Field key in conditionDic.Keys)
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
            ChangePropertys.Remove(_USER_ID);
            if (ChangePropertys.Count == 0)
            {
                return true;
            }

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TNET_USER SET");
            while (ChangePropertys.MoveNext())
            {
                sql.AppendFormat(" {0}{1}=:{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
            sql.Append(" WHERE USER_ID=:USER_ID");
            AddParameter(_USER_ID, DataRow[_USER_ID]);
            if (!string.IsNullOrEmpty(condition))
            {
                sql.AppendLine(" AND " + condition);
            }
            bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
        }
        public bool SelectByUserCode(string user_code)
        {
            string condition = " USER_CODE=:USER_CODE";
            AddParameter(_USER_CODE, user_code);
            return SelectByCondition(condition);
        }
        public bool SelectByEMail(string e_mail)
        {
            string condition = " E_MAIL=:E_MAIL";
            AddParameter(_E_MAIL, e_mail);
            return SelectByCondition(condition);
        }
        public bool SelectByMobileNo(string mobile_no)
        {
            string condition = " MOBILE_NO=:MOBILE_NO";
            AddParameter(_MOBILE_NO, mobile_no);
            return SelectByCondition(condition);
        }
        public bool SelectByPk(int user_id)
        {
            string condition = " USER_ID=:USER_ID";
            AddParameter(_USER_ID, user_id);
            return SelectByCondition(condition);
        }
        #endregion
    }

    public partial class Tnet_UserCollection : DataAccessCollectionBase
    {
        #region 构造和基本
        public Tnet_UserCollection() : base()
        {
        }

        protected override DataTable BuildTable()
        {
            return new Tnet_User().CloneSchemaOfTable();
        }
        protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tnet_User(DataTable.Rows[index]);
        }
        protected override string TableName
        {
            get { return Tnet_User._TableName; }
        }
        public Tnet_User this[int index]
        {
            get { return new Tnet_User(DataTable.Rows[index]); }
        }
        public enum Field
        {
            User_Id = 0,
            User_Code = 1,
            User_Nickname = 2,
            User_Name = 3,
            Father_Id = 4,
            User_Status = 5,
            User_Level = 6,
            E_Mail = 7,
            Mobile_No = 8,
            Auth_Status = 9,
            Auth_Time = 10,
            Login_Password = 11,
            Payment_Password = 12,
            Photo_Url = 13,
            Data_Source = 14,
            Remarks = 15,
            Create_Time = 16,
        }
        #endregion
        #region 基本方法
        protected bool ListByCondition(string condition)
        {
            string sql = "SELECT USER_ID,USER_CODE,USER_NICKNAME,USER_NAME,FATHER_ID,USER_STATUS,USER_LEVEL,E_MAIL,MOBILE_NO,AUTH_STATUS,AUTH_TIME,LOGIN_PASSWORD,PAYMENT_PASSWORD,PHOTO_URL,DATA_SOURCE,REMARKS,CREATE_TIME FROM TNET_USER WHERE " + condition;
            return ListBySql(sql);
        }

        public bool ListAll()
        {
            string condition = " 1=1";
            return ListByCondition(condition);
        }
        #region Linq
        public Tnet_User Find(Predicate<Tnet_User> match)
        {
            foreach (Tnet_User item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tnet_UserCollection FindAll(Predicate<Tnet_User> match)
        {
            Tnet_UserCollection list = new Tnet_UserCollection();
            foreach (Tnet_User item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tnet_User> match)
        {
            foreach (Tnet_User item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
        public bool DeleteAt(Predicate<Tnet_User> match)
        {
            BeginTransaction();
            foreach (Tnet_User item in this)
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