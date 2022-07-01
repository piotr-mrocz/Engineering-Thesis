export interface UserConversationDto {
    idSender?: number,
    sender?: string,
    idAddressee?: number,
    addressee?: string,
    content?: string,
    sendDate?: Date,
    senderPhotoName?: string
}
