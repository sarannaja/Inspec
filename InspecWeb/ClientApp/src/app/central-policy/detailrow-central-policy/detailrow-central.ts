// Generated by https://quicktype.io

export interface Detailrow {
    id:                     number;
    fiscalYearNewId:        number;
    fiscalYearNew:          FiscalYearNew;
    typeexaminationplanId:  number;
    typeexaminationplan:    Typeexaminationplan;
    title:                  string;
    startDate:              string;
    endDate:                string;
    createdAt:              string;
    createdBy:              string;
    user:                   User;
    status:                 Status;
    class:                  string;
    updateAt:               string;
    subjects:               any[];
    centralPolicyFiles:     CentralPolicyFile[];
    centralPolicyUser:      null;
    centralPolicyDates:     CentralPolicyDate[];
    centralPolicyProvinces: CentralPolicyProvince[];
    subjectGroups:          null;
    centralPolicyEvents:    null;
}

export interface CentralPolicyDate {
    id:              number;
    centralPolicyId: number;
    startDate:       string;
    endDate:         string;
}

export interface CentralPolicyFile {
    id:              number;
    centralPolicyId: number;
    name:            string;
    type:            null;
    description:     string;
}

export interface CentralPolicyProvince {
    id:                            number;
    centralPolicyId:               number;
    provinceId:                    number;
    province:                      Province;
    step:                          Step;
    status:                        Status;
    active:                        number;
    questionPeople:                null;
    subjectCentralPolicyProvinces: null;
    answerCentralPolicyProvinces:  null;
}

export interface Province {
    id:               number;
    sectorId:         number;
    sectors:          null;
    provincesGroupId: number;
    provincesGroups:  null;
    name:             string;
    nameEN:           string;
    shortnameEN:      string;
    shortnameTH:      null;
    link:             string;
    createdAt:        null;
}

export enum Status {
    ร่างกำหนดการ = "ร่างกำหนดการ",
}

export enum Step {
    มอบหมายเขต = "มอบหมายเขต",
}

export interface FiscalYearNew {
    id:              number;
    year:            number;
    createdAt:       string;
    startDate:       string;
    endDate:         string;
    centralPolicies: any[];
}

export interface Typeexaminationplan {
    id:        number;
    name:      string;
    createdAt: null;
}

export interface User {
    role_id:                number;
    position:               string;
    prefix:                 string;
    name:                   string;
    firstnameth:            null;
    lastnameth:             null;
    firstnameen:            null;
    lastnameen:             null;
    educational:            string;
    birthday:               string;
    officephonenumber:      string;
    telegraphnumber:        string;
    sideId:                 number;
    sides:                  null;
    ministryId:             number;
    ministries:             Departments;
    departmentId:           number;
    departments:            Departments;
    provincialDepartmentId: number;
    provincialDepartments:  null;
    provinceId:             number;
    province:               Province;
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
    userTokenMobiles:       null;
    userProvince:           null;
    notification:           null;
    signature:              null;
    commandnumber:          null;
    commandnumberdate:      null;
    fiscalYearId:           number;
    autocreateuser:         number;
    pw:                     string;
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

export interface Departments {
    id:           number;
    ministryId?:  number;
    ministries?:  Departments;
    name:         string;
    shortnameTH:  null;
    nameEN:       string;
    shortnameEN:  string;
    createdAt:    null;
    num?:         number;
    departments?: Departments[];
}
