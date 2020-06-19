export interface Report1 {
  title: string
  createdBy: string
  centralPolicyProvinces: Array<centralPolicyProvinces>
}
export interface centralPolicyProvinces {
  subjectCentralPolicyProvinces: Array<subjectCentralPolicyProvinces>
  name: string
  province:province
}
// export interface subjectCentralPolicyProvinces {
//   subquestionCentralPolicyProvinces: Array<subquestionCentralPolicyProvinces>
//   name: string
// }

export interface subjectCentralPolicyProvinces {
  name: string
  status: string
  type: string
  subquestionCentralPolicyProvinces: Array<subquestionCentralPolicyProvinces>
}

export interface subquestionCentralPolicyProvinces {
  subjectCentralPolicyProvinceGroups: Array<subjectCentralPolicyProvinceGroups>
  name: string
}




export interface subjectCentralPolicyProvinceGroups {

  provincialDepartment: provincialDepartment
}

export interface provincialDepartment {

  name: string
}

export interface province {

  name: string
}
