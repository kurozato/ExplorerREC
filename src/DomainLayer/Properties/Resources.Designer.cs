//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlackSugar.Properties {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BlackSugar.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   すべてについて、現在のスレッドの CurrentUICulture プロパティをオーバーライドします
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   /*** ExplorerRecodes ***/
        ///CREATE TABLE IF NOT EXISTS ExplorerRecodes(
        ///    ID INTEGER PRIMARY KEY AUTOINCREMENT,
        ///    Name TEXT,
        ///    Path TEXT,
        ///    DataSrt INTEGER
        ///);
        ///
        ////*** Index of ExplorerRecodes ***/
        ///CREATE TABLE IF NOT EXISTS ExplorerRecodes_INDEX (
        ///    Recode_ID INTEGER 
        ///);
        /// に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string CreateTable_ExplorerRecodes {
            get {
                return ResourceManager.GetString("CreateTable_ExplorerRecodes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Delete From ExplorerRecodes Where Path = &apos;@Path&apos;; に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string DeleteExplorerRecodes {
            get {
                return ResourceManager.GetString("DeleteExplorerRecodes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   /***Query***/
        ///SELECT
        ///     Name
        ///    ,Path
        ///FROM
        ///    ExplorerRecodes
        ///ORDER BY
        ///    DataSrt ASC
        /// に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string Query_ExplorerRecodes {
            get {
                return ResourceManager.GetString("Query_ExplorerRecodes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   /*Regist Model*/
        ///Delete From ExplorerRecodes Where Path = &apos;@Path&apos;;
        ///
        ///Update ExplorerRecodes Set 
        ///    DataSrt = DataSrt + 1;
        ///
        ///Insert Into ExplorerRecodes(
        ///    Name, 
        ///    Path, 
        ///    DataSrt
        ///)
        ///Values(
        ///    &apos;@Name&apos;,
        ///    &apos;@Path&apos;,
        ///     0);
        ///
        ///--make Index 
        ///Delete From ExplorerRecodes_INDEX;
        ///
        ///Insert Into ExplorerRecodes_INDEX(
        ///    Recode_ID
        ///)
        ///Select
        ///    ID
        ///From
        ///    ExplorerRecodes
        ///Order By
        ///    DataSrt;
        ///
        ///--delete 30 recodes over
        ///Delete From ExplorerRecodes
        ///Where ID In(
        ///    Select 
        ///      [残りの文字列は切り詰められました]&quot;; に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string RegistExplorerRecodes {
            get {
                return ResourceManager.GetString("RegistExplorerRecodes", resourceCulture);
            }
        }
    }
}
