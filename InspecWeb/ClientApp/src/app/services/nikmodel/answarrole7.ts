
// Generated by https://quicktype.io

export interface Answerrole7 {
    id:                   number;
    centralPolicyEventId: number;
    centralPolicyEvent:   null;
    userId:               string;
    user:                 null;
    status:               string;
    createdAt:            string;
}

// Generated by https://quicktype.io

export interface GetQuestionPeople {
    id:                   number;
    centralPolicyEventId: number;
    centralPolicyEvent:   CentralPolicyEvent;
    questionPeople:       string;
    notificationDate:     string;
    deadlineDate:         string;
}

export interface CentralPolicyEvent {
    id:                    number;
    centralPolicyId:       number;
    centralPolicy:         CentralPolicy;
    inspectionPlanEventId: number;
    inspectionPlanEvent:   InspectionPlanEvent;
    startDate:             string;
    endDate:               string;
    notificationDate:      string;
    deadlineDate:          string;
    haveSubject:           number;
}

export interface CentralPolicy {
    id:                     number;
    fiscalYearId:           number;
    fiscalYear:             null;
    title:                  string;
    startDate:              string;
    endDate:                string;
    createdAt:              string;
    createdBy:              string;
    status:                 string;
    type:                   string;
    class:                  string;
    subjects:               null;
    centralPolicyFiles:     null;
    centralPolicyUser:      null;
    centralPolicyDates:     null;
    centralPolicyProvinces: CentralPolicyProvince[];
    centralPolicyEvents:    any[];
}

export interface CentralPolicyProvince {
    id:                            number;
    centralPolicyId:               number;
    provinceId:                    number;
    province:                      Province;
    step:                          string;
    status:                        string;
    questionPeople:                null;
    subjectCentralPolicyProvinces: null;
    answerCentralPolicyProvinces:  null;
}

export interface Province {
    id:        number;
    name:      string;
    link:      string;
    createdAt: null;
}

export interface InspectionPlanEvent {
    id:                  number;
    provinceId:          number;
    province:            Province;
    startDate:           string;
    endDate:             string;
    createdAt:           string;
    createdBy:           string;
    roleCreatedBy:       string;
    status:              string;
    centralPolicies:     null;
    centralPolicyEvents: any[];
    centralPolicyUsers:  null;
}