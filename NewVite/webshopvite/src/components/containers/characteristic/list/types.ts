export interface ICharacteristicItem {
    id: number,
    name: string,
    categories: ICategoryItem[] | null,
}
export interface ICategoryItem {
    id: number,
    name: string,
}