export interface Regions {
    Id: number,
    Name: string,
    FiscalYears: Array<FiscalYears>
}

export interface FiscalYears {
    Year: number,
    Name: string,
    Projects: {
        Count: number,
        Completed: number,
        TotalBudget: number,
        TotalAmount: number,
        TotalSpent: number,
        TotalPercent: number
    },
    Provinces: Array<Province>
}

export interface Province {
    Id: number,
    Name: string,
}