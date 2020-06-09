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

export interface ProvinceFiscalYear {
    fiscalYears: Array<ProvinceFiscalYears>


}
export interface ProvinceFiscalYears {

    year: number,
    name: string,
    projects: {
        count: number,
        completed: number,
        totalBudget: number,
        totalAmount: number,
        totalSpent: number,
        totalPercent: number
    },
    // rovinces: Array<Province>
    region: {
        id: number,
        name: string
    },

}
export interface Province {
    id: number,
    name: string,
    iso: string,
}

//Ministers model
export interface Ministers {


    id: number,
    name: string,
    photo: string,
    position: string,
    positionShort: string,
    cabinet: Cabinet,
    fiscalYears: Array<MinisterfiscalYears>

}
export interface Cabinet {
    id: number,
    name: string
}

export interface MinisterRegions {
    id: number,
    name: string
}
export interface MinisterfiscalYears {

    year: number,
    name: string,
    projects: {
        count: number,
        completed: number,
        totalBudget: number,
        totalAmount: number,
        totalSpent: number,
        totalPercent: number
    },
    regions: Array<MinisterRegions>

}

export interface Cabinets {
    id: number,
    name: string
    ministers: Array<{
        year: string,
        name: string,
        position: string,
        positionShort: string,
    }>,
    fiscalYears: Array<{
        year: number,
        name: string,
        projects: {
            Count: number,
            Completed: number,
            TotalBudget: number,
            TotalAmount: number,
            TotalSpent: number,
            TotalPercent: number
        }
    }>


}




