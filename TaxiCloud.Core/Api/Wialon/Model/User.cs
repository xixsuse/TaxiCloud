// ReSharper disable UnusedMember.Global
namespace TaxiCloud.Core.Api.Wialon.Model
{
    public class UserInfo
    {
        public string Host { get; set; }
        public string Eid { get; set; }
        public string GisSid { get; set; }
        public string Au { get; set; }
        public int Tm { get; set; }
        public string WsdkVersion { get; set; }
        public string BaseUrl { get; set; }
        public string HwGwIp { get; set; }
        public User User { get; set; }
        public string Token { get; set; }
        public string Th { get; set; }
        public Classes Classes { get; set; }
        public Features Features { get; set; }
    }

    public class User
    {
        public string Nm { get; set; }
        public int Cls { get; set; }
        public int Id { get; set; }
        public Prp Prp { get; set; }
        public int Crt { get; set; }
        public int Bact { get; set; }
        public int Mu { get; set; }
        public int Ct { get; set; }
        public Ftp Ftp { get; set; }
        public int Fl { get; set; }
        public string Hm { get; set; }
        public int Ld { get; set; }
        public int Pfl { get; set; }
        public object Ap { get; set; }
        public Mapps Mapps { get; set; }
        public int Mappsmax { get; set; }
        public int Uacl { get; set; }
    }

    public class Prp
    {
        public string AppLogbookSettings { get; set; }
        public string SensolatorResourceId { get; set; }
        public string AccessTemplates { get; set; }
        public string AddrProvider { get; set; }
        public string Autocomplete { get; set; }
        public string CelColors { get; set; }
        public string Cfmt { get; set; }
        public string City { get; set; }
        public string DashboardWidgets { get; set; }
        public string Drvsvlist { get; set; }
        public string Dst { get; set; }
        public string Email { get; set; }
        public string EventDescr { get; set; }
        public string EvtFlags { get; set; }
        public string Fpnl { get; set; }
        public string GeodataSource { get; set; }
        public string Hbacit { get; set; }
        public string Hpnl { get; set; }
        public string Language { get; set; }
        public string LastTailColor { get; set; }
        public string LastTailPoints { get; set; }
        public string LastTailWidth { get; set; }
        public string Lastmsgl { get; set; }
        public string Locator { get; set; }
        public string MGu { get; set; }
        public string MMl { get; set; }
        public string MMonu { get; set; }
        public string MblGeolocation { get; set; }
        public string MblMpType { get; set; }
        public string MblUiVisibility { get; set; }
        public string MblUmMask { get; set; }
        public string MblUnVisibility { get; set; }
        public string MblUtMsgParams { get; set; }
        public string MblUtSensors { get; set; }
        public string MblUwLst { get; set; }
        public string MfUseSensors { get; set; }
        public string MinimapZoomLevel { get; set; }
        public string Mondv { get; set; }
        public string Mongr { get; set; }
        public string Mont { get; set; }
        public string Montr { get; set; }
        public string Monu { get; set; }
        public string Monuei { get; set; }
        public string Monueig { get; set; }
        public string Monuexpg { get; set; }
        public string Monug { get; set; }
        public string Monugr { get; set; }
        public string Monugv { get; set; }
        public string Monuv { get; set; }
        public string Msakey { get; set; }
        public string Msc { get; set; }
        public string MsgAw { get; set; }
        public string Mtagis { get; set; }
        public string Mtg { get; set; }
        public string Mtgis2 { get; set; }
        public string Mthere { get; set; }
        public string Mthtr { get; set; }
        public string Mtlux { get; set; }
        public string Mtly { get; set; }
        public string Mtmyin { get; set; }
        public string Mtmyin2 { get; set; }
        public string Mtnavm { get; set; }
        public string Mtosm { get; set; }
        public string Mtve { get; set; }
        public string Mtwgis2M { get; set; }
        public string Mtwikim { get; set; }
        public string Mtya { get; set; }
        public string Mtyahin { get; set; }
        public string MuCmdBtn { get; set; }
        public string MuDeleteFromList { get; set; }
        public string MuDevCfg { get; set; }
        public string MuDriver { get; set; }
        public string MuEvents { get; set; }
        public string MuFastReport { get; set; }
        public string MuFastReportIval { get; set; }
        public string MuFastReportTmpl { get; set; }
        public string MuFastTrackIval { get; set; }
        public string MuGprs { get; set; }
        public string MuGps { get; set; }
        public string MuGpsMode { get; set; }
        public string MuGpsTime { get; set; }
        public string MuLocMode { get; set; }
        public string MuLocation { get; set; }
        public string MuMessagesFilterIval { get; set; }
        public string MuMove { get; set; }
        public string MuMsgs { get; set; }
        public string MuPhoto { get; set; }
        public string MuReps { get; set; }
        public string MuRoute { get; set; }
        public string MuSens { get; set; }
        public string MuSms { get; set; }
        public string MuTblSort { get; set; }
        public string MuTracks { get; set; }
        public string MuTracksIval { get; set; }
        public string MuTrailer { get; set; }
        public string MuWatch { get; set; }
        public string Muf { get; set; }
        public string Mugow { get; set; }
        public string Muow { get; set; }
        public string Pbsd { get; set; }
        public string Pcal { get; set; }
        public string Plg { get; set; }
        public string Plsn { get; set; }
        public string Poil { get; set; }
        public string Poisrv { get; set; }
        public string Radd { get; set; }
        public string RouteProvider { get; set; }
        public string ShowLog { get; set; }
        public string Tapps { get; set; }
        public string TappsOrder { get; set; }
        public string TracksPlayerShowParams { get; set; }
        public string TracksPlayerShowSensors { get; set; }
        public string Trlsvlist { get; set; }
        public string Tz { get; set; }
        public string Umap { get; set; }
        public string Ursstp { get; set; }
        public string UsAddrFmt { get; set; }
        public string UsAddrOrdr { get; set; }
        public string UsUnits { get; set; }
        public string UsedHw { get; set; }
        public string UserSettingsHotkeys { get; set; }
        public string UserUnitCmds { get; set; }
        public string Usuei { get; set; }
        public string Vsplit { get; set; }
        public string VsplitBlockLeftPanel { get; set; }
        public string VsplitMessagesFilterTarget { get; set; }
        public string VsplitMonitoringMapTarget { get; set; }
        public string VsplitReportTemplatesFilterTarget { get; set; }
        public string Wdcheck { get; set; }
        public string WebgC { get; set; }
        public string WebgCId { get; set; }
        public string WlMpl { get; set; }
        public string WlShcu { get; set; }
        public string WlShel { get; set; }
        public string WlShhl { get; set; }
        public string WlShnu { get; set; }
        public string WlShom { get; set; }
        public string WlShtu { get; set; }
        public string WlSno { get; set; }
        public string WlUnits { get; set; }
        public string Zlst { get; set; }
        public string Znsn { get; set; }
        public string Znsrv { get; set; }
        public string Znsvlist { get; set; }
    }

    public class Ftp
    {
        public int Ch { get; set; }
        public int Tp { get; set; }
        public int Fl { get; set; }
    }

    public class Mapps
    {
        // ReSharper disable once InconsistentNaming
        public Dtidn _1 { get; set; }
    }

    public class Dtidn
    {
        public int Id { get; set; }
        public string N { get; set; }
        public string Uid { get; set; }
        public Cp Cp { get; set; }
        public As As { get; set; }
        public int E { get; set; }
    }

    public class Cp
    {
        public int Ui { get; set; }
        public string Un { get; set; }
    }

    public class As
    {
        public string Appid { get; set; }
        public string Device { get; set; }
        public string Type { get; set; }
    }

    public class Classes
    {
        public int AvlHw { get; set; }
        public int AvlResource { get; set; }
        public int AvlRetranslator { get; set; }
        public int AvlRoute { get; set; }
        public int AvlUnit { get; set; }
        public int AvlUnitGroup { get; set; }
        public int User { get; set; }
    }

    public class Features
    {
        public int Unlim { get; set; }
        public Svcs Svcs { get; set; }
    }

    public class Svcs
    {
        public int GurtamTest { get; set; }
        public int GurtamTest2 { get; set; }
        public int GurtamTest4 { get; set; }
        public int GurtamTest5 { get; set; }
        public int GurtamTest6 { get; set; }
        public int GurtamTest7 { get; set; }
        public int AdminFields { get; set; }
        public int App136614425274 { get; set; }
        public int App139453718848780 { get; set; }
        public int App139603203813884 { get; set; }
        public int App139603203813885 { get; set; }
        public int App139603236113919 { get; set; }
        public int App139603270613955 { get; set; }
        public int App139720525911943 { get; set; }
        public int App139876515376710 { get; set; }
        public int App139939404314612 { get; set; }
        public int App1401173879105398 { get; set; }
        public int App140714863982956 { get; set; }
        public int App14097435848385 { get; set; }
        public int App141655798427606 { get; set; }
        public int App142175403345873 { get; set; }
        public int App14231180337076 { get; set; }
        public int App14254657578430 { get; set; }
        public int App14255485439233 { get; set; }
        public int App14272033066005 { get; set; }
        public int App142908358039125 { get; set; }
        public int App1442144432246832 { get; set; }
        public int App144705967691348 { get; set; }
        public int App144729828010272 { get; set; }
        public int App1450165661109424 { get; set; }
        public int App14544434826387 { get; set; }
        public int App1457086586410271 { get; set; }
        public int App1457342506351167 { get; set; }
        public int App1457361558389422 { get; set; }
        public int App1459977018115109 { get; set; }
        public int App1461246494481125 { get; set; }
        public int App1470057822251326 { get; set; }
        public int App147938411238793 { get; set; }
        public int App147947339770405 { get; set; }
        public int App148405947197647 { get; set; }
        public int App148542775227880 { get; set; }
        public int App14885206221833 { get; set; }
        public int App14885206221834 { get; set; }
        public int App14885206221835 { get; set; }
        public int App14885206221836 { get; set; }
        public int App14885206221837 { get; set; }
        public int App148903678810096 { get; set; }
        public int App14908199453314 { get; set; }
        public int App149276317262781 { get; set; }
        public int App14937347015737 { get; set; }
        public int App1497618911303815 { get; set; }
        public int App1503463979179153 { get; set; }
        public int App1503650821244060 { get; set; }
        public int App1505380318248273 { get; set; }
        public int App15059830531345530 { get; set; }
        public int App150783054037216 { get; set; }
        public int App1509961607503146 { get; set; }
        public int App1512480576353908 { get; set; }
        public int App1514907610484369 { get; set; }
        public int AvlResource { get; set; }
        public int AvlUnit { get; set; }
        public int AvlUnitGroup { get; set; }
        public int AvlVin { get; set; }
        public int CmsManager { get; set; }
        public int CmsManager2 { get; set; }
        public int CreateResources { get; set; }
        public int CreateUnitGroups { get; set; }
        public int CreateUnits { get; set; }
        public int CreateUsers { get; set; }
        public int CustomFields { get; set; }
        public int CustomReports { get; set; }
        public int DriverGroups { get; set; }
        public int Drivers { get; set; }
        public int Ecodriving { get; set; }
        public int EmailNotification { get; set; }
        public int EmailReport { get; set; }
        public int GoogleService { get; set; }
        public int Hosting { get; set; }
        public int Hosting2 { get; set; }
        public int Hosting3 { get; set; }
        public int Hosting4 { get; set; }
        public int Hosting5 { get; set; }
        public int Hosting6 { get; set; }
        public int Hosting7 { get; set; }
        public int Hosting8 { get; set; }
        public int HostingArctic { get; set; }
        public int HostingBlack { get; set; }
        public int HostingIndigo { get; set; }
        public int HostingPlum { get; set; }
        public int HostingSummer { get; set; }
        public int HostingUrban { get; set; }
        public int ImportExport { get; set; }
        public int Jobs { get; set; }
        public int Lite { get; set; }
        public int Locator { get; set; }
        public int Maneuvers { get; set; }
        public int Messages { get; set; }
        public int MobileApps { get; set; }
        public int Nimbus { get; set; }
        public int Notifications { get; set; }
        public int Orders { get; set; }
        public int OwnGoogleService { get; set; }
        public int OwnYandexService { get; set; }
        public int Pois { get; set; }
        public int ProfileFields { get; set; }
        public int QuickGuide { get; set; }
        public int Reports { get; set; }
        public int Reportsdata { get; set; }
        public int Reportsmngt { get; set; }
        public int Reporttemplates { get; set; }
        public int RetranslatorUnits { get; set; }
        public int Rounds { get; set; }
        public int RouteSchedules { get; set; }
        public int Sdk { get; set; }
        public int ServiceGurtamTest { get; set; }
        public int ServiceIntervals { get; set; }
        public int Sms { get; set; }
        public int StorageUser { get; set; }
        public int Tacho { get; set; }
        public int TagGroups { get; set; }
        public int Tags { get; set; }
        public int TollRoads { get; set; }
        public int TrailerGroups { get; set; }
        public int Trailers { get; set; }
        public int UnitCommands { get; set; }
        public int UnitSensors { get; set; }
        public int UserNotifications { get; set; }
        public int Video { get; set; }
        public int Wialonhosting { get; set; }
        public int WialonActivex { get; set; }
        public int WialonHostingApi { get; set; }
        public int WialonHostingAppsApi { get; set; }
        public int WialonHostingDevApi { get; set; }
        public int WialonHostingTestApi { get; set; }
        public int WialonMobile { get; set; }
        public int WialonMobile2 { get; set; }
        public int WialonMobileClient { get; set; }
        public int Wialontest { get; set; }
        public int YandexPanorama { get; set; }
        public int YandexService { get; set; }
        public int ZoneGroups { get; set; }
        public int ZonesLibrary { get; set; }
    }
}