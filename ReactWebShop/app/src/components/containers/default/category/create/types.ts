export interface ICategoryCreate {
    name: string,
    image: string,
    description: string,
    priority: number,
    parentId: number
}
export interface ICategoryCreateError {
    name: string,
    description: string,
    image: string,
    priority: number
}