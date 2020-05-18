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
    Projects: {
        Count: number,
        Completed: number,
        TotalBudget: number,
        TotalAmount: number,
        TotalSpent: number,
        TotalPercent: number
    },
    regions: Array<MinisterRegions>

}



