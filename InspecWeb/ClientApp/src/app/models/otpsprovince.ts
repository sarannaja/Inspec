
export interface OtpsProvineFiscalYear {
    fiscalYears: FiscalYear[];
}

export interface FiscalYear {
    year:     number;
    name:     string;
    projects: Projects;
    region:   Region;
}

export interface Projects {
    count:        number;
    completed:    number;
    totalBudget:  number;
    totalAmount:  number;
    totalSpent:   number;
    totalPercent: number;
}

export interface Region {
    id:   number;
    name: string;
}
