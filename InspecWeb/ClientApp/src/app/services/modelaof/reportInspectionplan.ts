// Generated by https://quicktype.io

export interface Inslpectionplan {
  calendar: Calendar[];
}

export interface CalendarInspectionPlanEvent {
  id: number;
  provinceId: number;
  province: null;
  startDate: string;
  endDate: string;
  createdAt: string;
  createdBy: string;
  roleCreatedBy: string;
  status: string;
  centralPolicies: null;
  centralPolicyEvents: Calendar[];
  centralPolicyUsers: FluffyCentralPolicyUser[];
  user:User
}

export interface StickyCentralPolicyUser {
  id: number;
  centralPolicyId: number;
  centralPolicy?: PurpleCentralPolicy;
  provinceId: number;
  province: null;
  inspectionPlanEventId: number;
  inspectionPlanEvent?: PurpleInspectionPlanEvent;
  centralPolicyGroupId: number;
  centralPolicyGroup: null;
  userId: string;
  status: string;
  report: null;
  forward: null;
  invitedBy: string;
  draftStatus: string
}

export interface FluffyUser {
  role_id: number;
  position: string;
  prefix: string;
  name: string;
  educational: string;
  birthday: string;
  officephonenumber: string;
  telegraphnumber: string;
  side: string;
  ministryId: number;
  ministries: null;
  departmentId: number;
  departments: null;
  provincialDepartmentId: number;
  provincialDepartments: null;
  provinceId: number;
  province: null;
  districtId: number;
  district: null;
  subdistrictId: number;
  subdistricts: null;
  housenumber: string;
  rold: string;
  alley: string;
  postalcode: string;
  img: string;
  createdAt: string;
  startdate: string;
  enddate: string;
  active: number;
  userRegion: null;
  centralPolicyUser: StickyCentralPolicyUser[];
  userProvince: null;
  notification: null;
  id: string;
  userName: string;
  normalizedUserName: string;
  email: string;
  normalizedEmail: string;
  emailConfirmed: boolean;
  passwordHash: string;
  securityStamp: string;
  concurrencyStamp: string;
  phoneNumber: string;
  phoneNumberConfirmed: boolean;
  twoFactorEnabled: boolean;
  lockoutEnd: null;
  lockoutEnabled: boolean;
  accessFailedCount: number;
}

export interface TentacledCentralPolicyUser {
  id: number;
  centralPolicyId: number;
  provinceId: number;
  province: null;
  inspectionPlanEventId: number;
  centralPolicyGroupId: number;
  centralPolicyGroup: null;
  userId: string;
  status: string;
  report: null;
  forward: null;
  invitedBy: string;
  draftStatus: string
  centralPolicy?: FluffyCentralPolicy;
}

export interface PurpleUser {
  role_id: number;
  position: string;
  prefix: string;
  name: string;
  educational: string;
  birthday: string;
  officephonenumber: string;
  telegraphnumber: string;
  side: string;
  ministryId: number;
  ministries: null;
  departmentId: number;
  departments: null;
  provincialDepartmentId: number;
  provincialDepartments: null;
  provinceId: number;
  province: null;
  districtId: number;
  district: null;
  subdistrictId: number;
  subdistricts: null;
  housenumber: string;
  rold: string;
  alley: string;
  postalcode: string;
  img: string;
  createdAt: string;
  startdate: string;
  enddate: string;
  active: number;
  userRegion: null;
  centralPolicyUser: TentacledCentralPolicyUser[];
  userProvince: null;
  notification: null;
  id: string;
  userName: string;
  normalizedUserName: string;
  email: string;
  normalizedEmail: string;
  emailConfirmed: boolean;
  passwordHash: string;
  securityStamp: string;
  concurrencyStamp: string;
  phoneNumber: string;
  phoneNumberConfirmed: boolean;
  twoFactorEnabled: boolean;
  lockoutEnd: null;
  lockoutEnabled: boolean;
  accessFailedCount: number;
}

export interface FluffyCentralPolicy {
  id: number;
  fiscalYearId: number;
  fiscalYear: null;
  title: string
  startDate: string;
  endDate: string;
  createdAt: string;
  createdBy: string;
  status: string;
  type: string
  class: string;
  subjects: null;
  centralPolicyFiles: null;
  centralPolicyUser: FluffyCentralPolicyUser[];
  centralPolicyDates: null;
  centralPolicyProvinces: null;
  centralPolicyEvents: Calendar[];
}

export interface FluffyCentralPolicyUser {
  id: number;
  centralPolicyId: number;
  provinceId: number;
  province: null;
  inspectionPlanEventId: number;
  centralPolicyGroupId: number;
  centralPolicyGroup: null;
  userId: string;
  status: string;
  report: null;
  forward: null;
  invitedBy: string;
  user?: PurpleUser;
  draftStatus: string
  centralPolicy?: FluffyCentralPolicy;
}

export interface PurpleCentralPolicy {
  id: number;
  fiscalYearId: number;
  fiscalYear: null;
  title: string
  startDate: string;
  endDate: string;
  createdAt: string;
  createdBy: string;
  status: string;
  type: string
  class: string;
  subjects: null;
  centralPolicyFiles: null;
  centralPolicyUser: FluffyCentralPolicyUser[];
  centralPolicyDates: null;
  centralPolicyProvinces: null;
  centralPolicyEvents: CentralPolicyCentralPolicyEvent[];
}

export interface InspectionPlanEventCentralPolicyEvent {
  id: number;
  centralPolicyId: number;
  centralPolicy: PurpleCentralPolicy;
  inspectionPlanEventId: number;
  startDate: string;
  endDate: string;
  notificationDate: string;
  deadlineDate: string;
  haveSubject: number;
}

export interface FluffyInspectionPlanEvent {
  id: number;
  provinceId: number;
  province: null;
  startDate: string;
  endDate: string;
  createdAt: string;
  createdBy: string;
  roleCreatedBy: string;
  status: string
  centralPolicies: null;
  centralPolicyEvents: InspectionPlanEventCentralPolicyEvent[];
  centralPolicyUsers: PurpleCentralPolicyUser[];
}

export interface PurpleCentralPolicyUser {
  id: number;
  centralPolicyId: number;
  provinceId: number;
  province: null;
  inspectionPlanEventId: number;
  inspectionPlanEvent?: FluffyInspectionPlanEvent;
  centralPolicyGroupId: number;
  centralPolicyGroup: null;
  userId: string;
  status: string;
  report: null;
  forward: null;
  invitedBy: string;
  user?: FluffyUser;
  draftStatus: string
  centralPolicy?: CalendarCentralPolicy;
}

export interface PurpleInspectionPlanEvent {
  id: number;
  provinceId: number;
  province: null;
  startDate: string;
  endDate: string;
  createdAt: string;
  createdBy: string;
  roleCreatedBy: string;
  status: string
  centralPolicies: null;
  centralPolicyEvents: Calendar[];
  centralPolicyUsers: PurpleCentralPolicyUser[];
}

export interface CentralPolicyCentralPolicyEvent {
  id: number;
  centralPolicyId: number;
  inspectionPlanEventId: number;
  startDate: string;
  endDate: string;
  notificationDate: string;
  deadlineDate: string;
  haveSubject: number;
  inspectionPlanEvent?: PurpleInspectionPlanEvent;
}

export interface CalendarCentralPolicy {
  id: number;
  fiscalYearId: number;
  fiscalYear: null;
  title: string
  startDate: string;
  endDate: string;
  createdAt: string;
  createdBy: string;
  status: string;
  type: string
  class: string;
  subjects: null;
  centralPolicyFiles: null;
  centralPolicyUser: PurpleCentralPolicyUser[];
  centralPolicyDates: null;
  centralPolicyProvinces: null;
  centralPolicyEvents: CentralPolicyCentralPolicyEvent[];
}

export interface Calendar {
  id: number;
  centralPolicyId: number;
  centralPolicy?: CalendarCentralPolicy;
  inspectionPlanEventId: number;
  inspectionPlanEvent?: CalendarInspectionPlanEvent;
  startDate: string;
  endDate: string;
  notificationDate: string;
  deadlineDate: string;
  haveSubject: number;
}


// Generated by https://quicktype.io

export interface User {
  role_id:                number;
  position:               string;
  prefix:                 string;
  name:                   string;
  educational:            string;
  birthday:               string;
  officephonenumber:      string;
  telegraphnumber:        string;
  side:                   string;
  ministryId:             number;
  ministries:             null;
  departmentId:           number;
  departments:            null;
  provincialDepartmentId: number;
  provincialDepartments:  null;
  provinceId:             number;
  province:               null;
  districtId:             number;
  district:               null;
  subdistrictId:          number;
  subdistricts:           null;
  housenumber:            string;
  rold:                   string;
  alley:                  string;
  postalcode:             string;
  img:                    string;
  createdAt:              string;
  startdate:              string;
  enddate:                string;
  active:                 number;
  userRegion:             null;
  centralPolicyUser:      null;
  userProvince:           null;
  notification:           null;
  id:                     string;
  userName:               string;
  normalizedUserName:     string;
  email:                  string;
  normalizedEmail:        string;
  emailConfirmed:         boolean;
  passwordHash:           string;
  securityStamp:          string;
  concurrencyStamp:       string;
  phoneNumber:            string;
  phoneNumberConfirmed:   boolean;
  twoFactorEnabled:       boolean;
  lockoutEnd:             null;
  lockoutEnabled:         boolean;
  accessFailedCount:      number;
}
